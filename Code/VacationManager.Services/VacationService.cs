using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Dates;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;

namespace VacationManager.Services
{
    public class VacationService : 
        IVacationRequestService, IVacationDaysService, IEmployeeService
    {
        private const long CurrentEmployeeId = 2;

        public VacationRequestDto CreateRequest(VacationRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            request.RequestNumber = _uniqueRequestIdTestData++;
            request.CreationDate = DateTime.Now;
            request.State = VacationRequestState.Submitted;
            request.EmployeeId = CurrentEmployeeId;
            
            _vacationRequestsTestData.Add(request);
            
            return request;
        }
        
        public void DeleteRequest(long id)
        {
            var request = _vacationRequestsTestData.SingleOrDefault(x => x.RequestNumber == id);
            if (request == null)
                throw new ApplicationException(string.Format(
                    "Request number {0} was not found. It must exist in order to be deleted.", id));

            if (request.State != VacationRequestState.Submitted)
                throw new ApplicationException(string.Format(
                    "Request {0} was already {1}, cannot be deleted anymore. It must be in submited state in order to be deleted.", 
                    id, request.State));

            _vacationRequestsTestData.Remove(request);
        }

        public void ChangeRequestState(long id, VacationRequestState toState)
        {
            var request = _vacationRequestsTestData.SingleOrDefault(x => x.RequestNumber == id);
            if (request == null)
                throw new ApplicationException(string.Format(
                    "Request number {0} was not found. It must exist in order to change it's state.", id));

            if (request.State != VacationRequestState.Submitted)
                throw new ApplicationException(string.Format(
                    "Request {0} was already {1}, cannot change it's state anymore. It must be in submited state in order to change it's state.", 
                    id, request.State));

            request.State = toState;
        }

        public List<VacationRequestDto> GetMyRequests()
        {
            return _vacationRequestsTestData
                .Where(x => x.EmployeeId == CurrentEmployeeId)
                .ToList();
        }
        
        public List<VacationRequestDto> GetPendingRequests()
        {
            var myEmployeeIds = _employeesTestData
                .Where(x => x.ManagerId == CurrentEmployeeId)
                .Select(x => x.Id);

            return _vacationRequestsTestData
                .Where(x =>
                    myEmployeeIds.Contains(x.EmployeeId) &&
                    (x.State == VacationRequestState.Submitted))
                .OrderByDescending(x => x.CreationDate)
                .ToList();
        }

        public VacationDaysDto GetVacationDays()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;

            return _vacationDaysTestData
                .SingleOrDefault(x => x.EmployeeId == CurrentEmployeeId);
        }

        #region VacationRequest TestData helpers

        private static long _uniqueRequestIdTestData = 1;
        private static readonly Random _randomTestDataGenerator = new Random(100);
        private static readonly IList<VacationRequestDto> _vacationRequestsTestData = BuildMockedRequests();

        private static IList<VacationRequestDto> BuildMockedRequests()
        {
            return Builder<VacationRequestDto>.CreateListOfSize(10)
                .All()
                    .With(x => x.RequestNumber = _uniqueRequestIdTestData++)
                    .And(x => x.State = (VacationRequestState)_randomTestDataGenerator.Next(1, 3))
                .TheFirst(2)
                    .With(x => x.EmployeeId = 1)
                    .Do(x => x.VacationDays.Add(December.The20th))
                    .Do(x => x.VacationDays.Add(December.The21st))
                .TheNext(5)
                    .With(x => x.EmployeeId = 2)
                    .Do(x => x.VacationDays.Add(December.The10th))
                .TheNext(2)
                    .With(x => x.EmployeeId = 1)
                    .Do(x => x.VacationDays.Add(December.The11th))
                .TheNext(1)
                    .With(x => x.EmployeeId = 3)
                    .Do(x => x.VacationDays.Add(December.The2nd))
                    .Do(x => x.VacationDays.Add(December.The3rd))
                    .Do(x => x.VacationDays.Add(December.The4th))
                .Build();
        }

        #endregion

        #region VacationDays TestData helpers

        private static readonly IList<VacationDaysDto> _vacationDaysTestData = BuildMockedVacationDays();

        private static IList<VacationDaysDto> BuildMockedVacationDays()
        {
            return Builder<VacationDaysDto>.CreateListOfSize(2)
                .TheFirst(1)
                    .With(x => x.EmployeeId = 1) // Costin Morariu
                    .And(x => x.Year = 2012)
                    .And(x => x.TotalNumber = 21)
                    .And(x => x.Taken = 21)
                    .And(x => x.Left = 0)
                    .And(x => x.Paid = 0)
                .TheNext(1)
                    .With(x => x.EmployeeId = 2) // Mihai Barabas
                    .And(x => x.Year = 2012)
                    .And(x => x.TotalNumber = 25)
                    .And(x => x.Taken = 21)
                    .And(x => x.Left = 4)
                    .And(x => x.Paid = 0)
                .Build();
        }

        #endregion

        #region Employee TestData helpers

        private static long _uniqueEmployeeIdTestData = 1;
        private static readonly IList<EmployeeDto> _employeesTestData = BuildMockedEmployees();

        private static IList<EmployeeDto> BuildMockedEmployees()
        {
            return Builder<EmployeeDto>.CreateListOfSize(6)
                .All()
                    .With(x => x.Id = _uniqueEmployeeIdTestData++)
                .TheFirst(1) // #1
                    .With(x => x.FirstName = "Costin")
                    .And(x => x.SurnameName = "Morariu")
                    .And(x => x.EmailAddress = "costin.morariu@iquestgroup.com")
                    .And(x => x.Roles = EmployeeRoles.Executive)
                    .And(x => x.ManagerId = 2) // Mihai Barabas
                .TheNext(1) // #2
                    .With(x => x.FirstName = "Mihai")
                    .And(x => x.SurnameName = "Barabas")
                    .And(x => x.EmailAddress = "mihai.barabas@iquestgroup.com")
                    .And(x => x.Roles = (EmployeeRoles.Executive | EmployeeRoles.Manager))
                    .And(x => x.ManagerId = 3) // Top Manager
                .TheNext(1) // #3
                    .With(x => x.FirstName = "Top")
                    .And(x => x.SurnameName = "Manager")
                    .And(x => x.EmailAddress = "top.manager@iquestgroup.com")
                    .And(x => x.Roles = EmployeeRoles.Manager)
                    .And(x => x.ManagerId = 0) // does not have
                .TheNext(1) // #4
                    .With(x => x.FirstName = "Hr")
                    .And(x => x.SurnameName = "Generic")
                    .And(x => x.EmailAddress = "hr_holidays@iquestgroup.com")
                    .And(x => x.Roles = EmployeeRoles.Hr)
                    .And(x => x.ManagerId = 0) // does not have
                .TheNext(1) // #5
                    .With(x => x.FirstName = "Ioana")
                    .And(x => x.SurnameName = "Sandu")
                    .And(x => x.EmailAddress = "ioana.sandu@iquestgroup.com")
                    .And(x => x.Roles = (EmployeeRoles.Executive | EmployeeRoles.Hr))
                    .And(x => x.ManagerId = 0) // does not have
                .TheNext(1) // #6
                    .With(x => x.FirstName = "Hr")
                    .And(x => x.SurnameName = "Manager")
                    .And(x => x.EmailAddress = "Hr.Manager@iquestgroup.com")
                    .And(x => x.Roles = (EmployeeRoles.Executive | EmployeeRoles.Manager | EmployeeRoles.Hr))
                    .And(x => x.ManagerId = 3) // Top Manager
                .Build();
        }

        #endregion
    }
}
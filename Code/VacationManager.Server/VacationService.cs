using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Dates;

namespace VacationManager.Server
{
    public class VacationService : 
        IVacationRequestService, IVacationDaysService, IEmployeeService
    {
        private const long CurrentEmployeeId = 1;

        public VacationService()
        {
            _vacationRequestsTestData = BuildMockedRequests();
            _vacationDaysTestData = BuildMockedVacationDays();
            _employeesTestData = BuildMockedEmployees();
        }

        public VacationRequestDto CreateRequest(VacationRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            request.RequestNumber = _uniqueRequestIdTestData++;
            request.CreationDate = DateTime.Now;
            request.State = RequestState.Submitted;
            request.EmployeeId = CurrentEmployeeId;
            
            _vacationRequestsTestData.Add(request);
            
            return request;
        }
        
        public void DeleteRequest(long id)
        {
            var request = _vacationRequestsTestData.SingleOrDefault(x => x.RequestNumber == id);
            if (request == null)
                throw new ApplicationException(string.Format("Request number {0} was not found.", id));

            _vacationRequestsTestData.Remove(request);
        }

        public void ChangeRequestState(long id, RequestState toState)
        {
            throw new NotImplementedException();
        }

        public List<VacationRequestDto> GetAllRequests()
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
                    (x.State == RequestState.Submitted))
                .ToList();
        }

        public VacationDaysDto GetVacationDays()
        {
            return _vacationDaysTestData
                .SingleOrDefault(x => x.EmployeeId == CurrentEmployeeId);
        }

        #region VacationRequest TestData helpers

        private long _uniqueRequestIdTestData = 1;
        private readonly Random _randomTestDataGenerator = new Random(100);
        private readonly IList<VacationRequestDto> _vacationRequestsTestData;

        private IList<VacationRequestDto> BuildMockedRequests()
        {
            return Builder<VacationRequestDto>.CreateListOfSize(10)
                .All()
                    .With(x => x.RequestNumber = _uniqueRequestIdTestData++)
                    .And(x => x.State = (RequestState)_randomTestDataGenerator.Next(1, 3))
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

        private readonly IList<VacationDaysDto> _vacationDaysTestData;

        private IList<VacationDaysDto> BuildMockedVacationDays()
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

        private long _uniqueEmployeeIdTestData = 1;
        private readonly IList<EmployeeDto> _employeesTestData;

        private IList<EmployeeDto> BuildMockedEmployees()
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
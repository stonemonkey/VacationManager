using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Dates;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;

namespace VacationManager.Services
{
    public class VacationService : 
        IVacationRequestService, IVacationDaysService, IEmployeeService
    {
        public VacationRequestDto CreateRequest(VacationRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            
            if (_employeesTestData.All(x => x.Id != request.EmployeeId))
                throw new ApplicationException(
                    string.Format("New request is associated with inexistent employee having id {0}.", request.EmployeeId));

            var numberOfVacationDays = request.VacationDays.Count();
            var numberOfVacationDaysLeft = _vacationDaysTestData.Single(x => x.EmployeeId == request.EmployeeId).Left;
            if ((numberOfVacationDays < 1) || (numberOfVacationDays > numberOfVacationDaysLeft))
                throw new ApplicationException(
                    string.Format("New request has invalid number of vacation days {0}. Must greather than 0 and less or equal than days left {1}.", numberOfVacationDays, numberOfVacationDays));

            request.RequestNumber = _uniqueRequestIdTestData++;
            request.CreationDate = DateTime.Now;
            request.State = VacationRequestState.Submitted;
            var employee = _employeesTestData.Single(x => x.Id == request.EmployeeId);
            request.EmployeeFullName = employee.Firstname + " " + employee.Surname;
            
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

        public List<VacationRequestDto> SearchRequests(VacationRequestSearchCriteriaDto criteria)
        {
            if (criteria == null)
                return _vacationRequestsTestData.ToList();

            IEnumerable<long> employeeIds;
            if (criteria.GetMine)
                employeeIds = new List<long> { criteria.EmployeeId };
            else
                employeeIds = _employeesTestData
                    .Where(x => x.ManagerId == criteria.EmployeeId)
                    .Select(x => x.Id);

            return _vacationRequestsTestData
                .Where(x =>
                    employeeIds.Contains(x.EmployeeId) &&
                    ((criteria.States == null) || criteria.States.Contains(x.State)))
                .OrderByDescending(x => x.CreationDate)
                .ToList();
        }

        public VacationDaysDto GetVacationDaysByEmployeeId(long employeeId)
        {
            return _vacationDaysTestData
                .SingleOrDefault(x => x.EmployeeId == employeeId);
        }

        public EmployeeDto GetEmployeeById(long id)
        {
            // just checking if we have here the caller identity
            //var userName = Thread.CurrentPrincipal.Identity.Name;

            return _employeesTestData.SingleOrDefault(x => x.Id == id);
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
                    .And(x => x.EmployeeFullName = "Costin Morariu")
                    .Do(x => x.VacationDays.Add(December.The20th))
                    .Do(x => x.VacationDays.Add(December.The21st))
                .TheNext(5)
                    .With(x => x.EmployeeId = 2)
                    .And(x => x.EmployeeFullName = "Mihai Barabas")
                    .Do(x => x.VacationDays.Add(December.The10th))
                .TheNext(2)
                    .With(x => x.EmployeeId = 1)
                    .And(x => x.EmployeeFullName = "Costin Morariu")
                    .Do(x => x.VacationDays.Add(December.The11th))
                .TheNext(1)
                    .With(x => x.EmployeeId = 5)
                    .And(x => x.EmployeeFullName = "Ioana Sandu")
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
                    .And(x => x.Taken = 18)
                    .And(x => x.Left = 2)
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
                    .With(x => x.Firstname = "Costin")
                    .And(x => x.Surname = "Morariu")
                    .And(x => x.EmailAddress = "costin.morariu@iquestgroup.com")
                    .And(x => x.Roles = EmployeeRoles.Executive)
                    .And(x => x.ManagerId = 2) // Mihai Barabas
                .TheNext(1) // #2
                    .With(x => x.Firstname = "Mihai")
                    .And(x => x.Surname = "Barabas")
                    .And(x => x.EmailAddress = "mihai.barabas@iquestgroup.com")
                    .And(x => x.Roles = (EmployeeRoles.Executive | EmployeeRoles.Manager))
                    .And(x => x.ManagerId = 3) // Top Manager
                .TheNext(1) // #3
                    .With(x => x.Firstname = "Top")
                    .And(x => x.Surname = "Manager")
                    .And(x => x.EmailAddress = "top.manager@iquestgroup.com")
                    .And(x => x.Roles = EmployeeRoles.Manager)
                    .And(x => x.ManagerId = 0) // does not have
                .TheNext(1) // #4
                    .With(x => x.Firstname = "Hr")
                    .And(x => x.Surname = "Generic")
                    .And(x => x.EmailAddress = "hr_holidays@iquestgroup.com")
                    .And(x => x.Roles = EmployeeRoles.Hr)
                    .And(x => x.ManagerId = 0) // does not have
                .TheNext(1) // #5
                    .With(x => x.Firstname = "Ioana")
                    .And(x => x.Surname = "Sandu")
                    .And(x => x.EmailAddress = "ioana.sandu@iquestgroup.com")
                    .And(x => x.Roles = (EmployeeRoles.Executive | EmployeeRoles.Hr))
                    .And(x => x.ManagerId = 0) // does not have
                .TheNext(1) // #6
                    .With(x => x.Firstname = "Hr")
                    .And(x => x.Surname = "Manager")
                    .And(x => x.EmailAddress = "Hr.Manager@iquestgroup.com")
                    .And(x => x.Roles = (EmployeeRoles.Executive | EmployeeRoles.Manager | EmployeeRoles.Hr))
                    .And(x => x.ManagerId = 3) // Top Manager
                .Build();
        }

        #endregion
    }
}
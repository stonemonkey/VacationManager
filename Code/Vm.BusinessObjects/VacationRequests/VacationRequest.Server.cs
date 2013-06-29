using System;
using System.Collections.Generic;
using System.Linq;
using VacationManager.Common.Model;
using VacationManager.Persistence;

namespace Vm.BusinessObjects.VacationRequests
{
    public partial class VacationRequest
    {
        #region DataPortal_XYZ methods

        /// <summary>
        /// Loads default values into current business object fields.
        /// </summary>
        protected override void DataPortal_Create()
        {
            VacationDays = new List<DateTime>();
        }

        /// <summary>
        /// Loads service object fields values (parameter) into the current business object 
        /// fields. Curent business object is a transient one. It may be persisted some time
        /// somewhere but for the moment we assume it is not and deal with it like transient.
        /// </summary>
        /// <param name="entity">The source.</param>
        protected void DataPortal_Create(VacationManager.Persistence.Model.VacationRequest entity)
        {
            _requestNumber = entity.RequestNumber;
            _submissionDate = entity.CreationDate;
            _stateId = entity.State;
            _employeeFullName = entity.Employee.Firstname + " " + entity.Employee.Surname;
            
            EmployeeId = entity.Employee.Id;
            // TODO: check if DateTime.Parse may introduce errors because of culture
            var vacationDays = entity.VacationDays
                .Split(new[] { VacationManager.Persistence.Model.VacationRequest.VacationDaysSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList()
                .ConvertAll(DateTime.Parse);
            VacationDays = new List<DateTime>(vacationDays);
        }

        /// <summary>
        /// Create new service object and persist it. For the moment we assume this
        /// means to submit a new vacation request.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            using (var ctx = new VacationManagerContext())
            {
                var employee = ctx.Employees.FirstOrDefault(x => x.Id == EmployeeId);
                if (employee == null)
                    throw new ApplicationException(
                        string.Format("New request is associated with inexistent employee having id {0}.", EmployeeId));

                var numberOfVacationDaysLeft = ctx.VacationStatus.Single(x => x.Employee.Id == EmployeeId).Left;
                if ((NumberOfDays < 1) || (NumberOfDays > numberOfVacationDaysLeft))
                    throw new ApplicationException(
                        string.Format("New request has invalid number of vacation days {0}. Must greather than 0 and less or equal than days left {1}.", NumberOfDays, NumberOfDays));

                var request = new VacationManager.Persistence.Model.VacationRequest
                {
                    State = VacationRequestState.Submitted,
                    Employee = employee,
                    VacationDays = Days,
                };

                ctx.Requests.Add(request);
                ctx.SaveChanges();

                _requestNumber = request.RequestNumber;
                _submissionDate = request.CreationDate;
                _stateId = request.State;
                _employeeFullName = employee.Firstname + " " + employee.Surname;
            }
        }

        /// <summary>
        /// Remove service object having parameter id. For the moment we assume this 
        /// means to cancel a submitted request.
        /// </summary>
        protected void DataPortal_Delete(long id)
        {
            using (var ctx = new VacationManagerContext())
            {
                var request = ctx.Requests.FirstOrDefault(x => x.RequestNumber == id);
                if (request == null)
                    throw new ApplicationException(string.Format(
                        "Request number {0} was not found. It must exist in order to be deleted.", id));

                if (request.State != VacationRequestState.Submitted)
                    throw new ApplicationException(string.Format(
                        "Request {0} was already {1}, cannot be deleted anymore. It must be in submited state in order to be deleted.",
                        id, request.State));

                ctx.Requests.Remove(request);
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}

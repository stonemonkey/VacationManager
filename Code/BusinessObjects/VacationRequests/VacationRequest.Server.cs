using System;
using System.Linq;
using Common.Model;
using Persistence;

namespace BusinessObjects.VacationRequests
{
    public partial class VacationRequest
    {
        #region DataPortal_XYZ methods

        /// <summary>
        /// Loads service object fields values (parameter) into the current business object 
        /// fields. Curent business object is a transient one. It may be persisted some time
        /// somewhere but for the moment we assume it is not and deal with it like transient.
        /// </summary>
        /// <param name="entity">The source.</param>
        protected void DataPortal_Create(Persistence.Model.VacationRequestEntity entity)
        {
            _requestNumber = entity.Id;
            _submissionDate = entity.CreationDate;
            _stateId = entity.State;
            _employeeFullName = entity.Employee.Firstname + " " + entity.Employee.LastName;
            
            EmployeeId = entity.Employee.Id;
            StartDate = entity.StartDate;
            EndDate = entity.EndDate;
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

                var numberOfVacationDaysLeft = ctx.Situations.Single(x => x.Employee.Id == EmployeeId).AvailableDays;
                if ((NumberOfDays < 1) || (NumberOfDays > numberOfVacationDaysLeft))
                    throw new ApplicationException(
                        string.Format("New request has invalid number of vacation days {0}. Must greather than 0 and less or equal than days left {1}.", NumberOfDays, NumberOfDays));

                var request = new Persistence.Model.VacationRequestEntity
                {
                    CreationDate = DateTime.Now, // TODO: utc?
                    StartDate = StartDate,
                    EndDate =  EndDate,
                    State = VacationRequestState.Submitted,
                    Employee = employee,
                };

                ctx.Requests.Add(request);
                ctx.SaveChanges();

                _requestNumber = request.Id;
                _submissionDate = request.CreationDate;
                _stateId = request.State;
                _employeeFullName = employee.Firstname + " " + employee.LastName;
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
                var request = ctx.Requests.FirstOrDefault(x => x.Id == id);
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

using System;
using System.Collections.Generic;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;
using Vm.BusinessObjects.Server;

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
        /// <param name="serviceObject">The source.</param>
        protected void DataPortal_Create(VacationRequestDto serviceObject)
        {
            _requestNumber = serviceObject.RequestNumber;
            _submissionDate = serviceObject.CreationDate;
            _stateId = serviceObject.State;
            _employeeFullName = serviceObject.EmployeeFullName;
            
            EmployeeId = serviceObject.EmployeeId;
            VacationDays = new List<DateTime>(serviceObject.VacationDays);
        }

        /// <summary>
        /// Create new service object and persist it. For the moment we assume this
        /// means to submit a new vacation request.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            var serviceObject = new VacationRequestDto
            {
                State = VacationRequestState.Submitted,
                EmployeeId = EmployeeId,
                VacationDays = VacationDays,
            };

            using (var proxy = new ServiceProxy<IVacationRequestService>(Configuration.ServiceAddress))
            {
                var createdServiceObject = proxy.GetChannel().CreateRequest(serviceObject);

                _requestNumber = createdServiceObject.RequestNumber;
                _submissionDate = createdServiceObject.CreationDate;
                _stateId = createdServiceObject.State;
                _employeeFullName = serviceObject.EmployeeFullName;

                EmployeeId = createdServiceObject.EmployeeId;
                VacationDays = createdServiceObject.VacationDays;
            }
        }

        /// <summary>
        /// Remove service object having parameter id. For the moment we assume this 
        /// means to cancel a submitted request.
        /// </summary>
        protected void DataPortal_Delete(long id)
        {
            using (var proxy = new ServiceProxy<IVacationRequestService>(Configuration.ServiceAddress))
            {
                proxy.GetChannel().DeleteRequest(id);
            }
        }

        #endregion
    }
}

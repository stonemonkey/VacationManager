using System;
using System.Collections.Generic;
using System.ServiceModel;
using Csla;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;

namespace VacationManager.Ui.BusinessObjects
{
    [Serializable]
    public class VacationRequest : BusinessBase<VacationRequest>
    {
        #region Private fields

        private long _requestNumber;
        private DateTime _submissionDate;
        private VacationRequestState _stateId = VacationRequestState.Submitted;
        private long _employeeId;

        #endregion

        #region Property definitions

        public static PropertyInfo<long> RequestNumberProperty =
            RegisterProperty<long>(c => c.RequestNumber, RelationshipTypes.PrivateField);

        public static PropertyInfo<DateTime> SubmissionDateProperty =
            RegisterProperty<DateTime>(c => c.SubmissionDate, RelationshipTypes.PrivateField);

        public static PropertyInfo<VacationRequestState> StateIdProperty =
            RegisterProperty<VacationRequestState>(c => c.StateId, RelationshipTypes.PrivateField);

        public static PropertyInfo<long> EmployeeIdProperty =
            RegisterProperty<long>(c => c.EmployeeId, RelationshipTypes.PrivateField);

        public static PropertyInfo<List<DateTime>> VacationDaysProperty =
            RegisterProperty<List<DateTime>>(c => c.VacationDays);

        #endregion

        #region Public properties

        public long RequestNumber
        {
            get { return GetProperty(RequestNumberProperty, _requestNumber); }
        }

        public DateTime SubmissionDate
        {
            get { return GetProperty(SubmissionDateProperty, _submissionDate); }
        }

        public string State
        {
            get { return StateId.ToString(); }
        }

        public VacationRequestState StateId
        {
            get { return GetProperty(StateIdProperty, _stateId); }
        }

        public long EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty, _employeeId); }
        }

        public int NumberOfDays
        {
            get
            {
                if (VacationDays != null)
                    return VacationDays.Count;

                return 0;
            }
        }

        public List<DateTime> VacationDays
        {
            get { return GetProperty(VacationDaysProperty); }
            set { SetProperty(VacationDaysProperty, value); }
        }

        #endregion

        #region DataPortal_XYZ methods

        /// <summary>
        /// Loads default values into current business object fields.
        /// </summary>
        private void DataPortal_Create()
        {
            VacationDays = new List<DateTime>();
        }

        /// <summary>
        /// Loads service object fields values (parameter) into the current business object 
        /// fields. Curent business object is a transient one. It may be persisted some time
        /// somewhere but for the moment we assume it is not and deal wit it like transient.
        /// </summary>
        /// <param name="serviceObject">The source.</param>
        private void DataPortal_Create(VacationRequestDto serviceObject)
        {
            _requestNumber = serviceObject.RequestNumber;
            _submissionDate = serviceObject.CreationDate;
            _stateId = serviceObject.State;
            _employeeId = serviceObject.EmployeeId;

            VacationDays = new List<DateTime>(serviceObject.VacationDays);
        }

        /// <summary>
        /// Create new service object and persist it. For the moment we assume this
        /// means to submit a new vacation request.
        /// </summary>
        private void DataPortal_Insert()
        {
            var serviceObject = new VacationRequestDto
            {
                State = VacationRequestState.Submitted,
                VacationDays = VacationDays,
            };

            using (var proxy = new ServiceProxy<IVacationRequestService>(Configuration.ServiceAddress))
            {
                var createdServiceObject = proxy.GetChannel().CreateRequest(serviceObject);

                _requestNumber = createdServiceObject.RequestNumber;
                _submissionDate = createdServiceObject.CreationDate;
                _stateId = createdServiceObject.State;
                _employeeId = createdServiceObject.EmployeeId;
            }
        }

        /// <summary>
        /// Remove service object having parameter id. For the moment we assume this 
        /// means to cancel a submitted request.
        /// </summary>
        private void DataPortal_Delete(long id)
        {
            using (var proxy = new ServiceProxy<IVacationRequestService>(Configuration.ServiceAddress))
            {
                proxy.GetChannel().DeleteRequest(id);
            }
        }
        #endregion
    }
}

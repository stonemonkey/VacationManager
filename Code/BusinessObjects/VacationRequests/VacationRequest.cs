using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective
using Common.Model;

namespace BusinessObjects.VacationRequests
{
    [Serializable]
    public partial class VacationRequest : BusinessBase<VacationRequest>
    {
        #region Private fields

        private long _requestNumber;
        private DateTime _submissionDate;
        private VacationRequestState _stateId = VacationRequestState.Submitted;
        private string _employeeFullName;

        #endregion
        
        #region Property definitions

        private static PropertyInfo<long> RequestNumberProperty =
            RegisterProperty<long>(c => c.RequestNumber, RelationshipTypes.PrivateField);

        private static PropertyInfo<DateTime> SubmissionDateProperty =
            RegisterProperty<DateTime>(c => c.SubmissionDate, RelationshipTypes.PrivateField);

        private static PropertyInfo<VacationRequestState> StateIdProperty =
            RegisterProperty<VacationRequestState>(c => c.StateId, RelationshipTypes.PrivateField);

        private static PropertyInfo<string> EmployeeFullNameProperty =
            RegisterProperty<string>(c => c.EmployeeFullName, RelationshipTypes.PrivateField);

        private static PropertyInfo<long> EmployeeIdProperty =
            RegisterProperty<long>(c => c.EmployeeId);

        private static PropertyInfo<DateTime> StartDateProperty =
            RegisterProperty<DateTime>(c => c.StartDate);

        private static PropertyInfo<DateTime> EndDateProperty =
            RegisterProperty<DateTime>(c => c.EndDate);

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

        public DateTime StartDate
        {
            get { return GetProperty(StartDateProperty); }
            set { SetProperty(StartDateProperty, value); }
        }

        public DateTime EndDate
        {
            get { return GetProperty(EndDateProperty); }
            set { SetProperty(EndDateProperty, value); }
        }

        public string State
        {
            get { return StateId.ToString(); }
        }

        public VacationRequestState StateId
        {
            get { return GetProperty(StateIdProperty, _stateId); }
        }

        public string EmployeeFullName
        {
            get { return GetProperty(EmployeeFullNameProperty, _employeeFullName); }
        }

        public long EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            set { SetProperty(EmployeeIdProperty, value); }
        }

        public int NumberOfDays
        {
            get
            {
                var dif = (EndDate - StartDate);
                // TODO: substract weekend and legal holydays 
                return (dif.Days + 1);
            }
        }
        
        #endregion
    
        /// <summary>
        /// Loads default values into current business object fields.
        /// This is common for client and server. Does not make sense to go to server for this.
        /// </summary>
        [RunLocal]
        protected override void DataPortal_Create()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            StartDate = tomorrow;
            EndDate = tomorrow;
        }
    }
}

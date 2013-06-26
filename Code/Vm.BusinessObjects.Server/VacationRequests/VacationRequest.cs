using System;
using System.Collections.Generic;
using System.Text;
using Csla;
using VacationManager.Common.DataContracts;

namespace Vm.BusinessObjects.VacationRequests
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

        public static PropertyInfo<long> RequestNumberProperty =
            RegisterProperty<long>(c => c.RequestNumber, RelationshipTypes.PrivateField);

        public static PropertyInfo<DateTime> SubmissionDateProperty =
            RegisterProperty<DateTime>(c => c.SubmissionDate, RelationshipTypes.PrivateField);

        public static PropertyInfo<VacationRequestState> StateIdProperty =
            RegisterProperty<VacationRequestState>(c => c.StateId, RelationshipTypes.PrivateField);

        public static PropertyInfo<string> EmployeeFullNameProperty =
            RegisterProperty<string>(c => c.EmployeeFullName, RelationshipTypes.PrivateField);

        public static PropertyInfo<long> EmployeeIdProperty =
            RegisterProperty<long>(c => c.EmployeeId);

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
        
        public string Days
        {
            get
            {
                var days = new StringBuilder();
                foreach (var d in VacationDays)
                {
                    days.Append(" ");
                    days.Append(d.ToString("dd/MM"));
                }
                return days.ToString();
            }
        }

        #endregion
    }
}

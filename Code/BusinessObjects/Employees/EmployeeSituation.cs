using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective

namespace BusinessObjects.Employees
{
    [Serializable]
    public partial class EmployeeSituation : BusinessBase<EmployeeSituation>
    {
        #region Private fields

        private int _consumedDays;
        private int _availableDays;

        #endregion

        #region Property definitions

        private static PropertyInfo<int> ConsumedDaysProperty =
            RegisterProperty<int>(c => c.ConsumedDays, RelationshipTypes.PrivateField);

        private static PropertyInfo<int> AvailableDaysProperty =
            RegisterProperty<int>(c => c.AvailableDays, RelationshipTypes.PrivateField);

        #endregion

        #region Public properties

        public int ConsumedDays
        {
            get { return GetProperty(ConsumedDaysProperty, _consumedDays); }
        }

        public int AvailableDays
        {
            get { return GetProperty(AvailableDaysProperty, _availableDays); }
        }

        public int TotalNumberOfDays
        {
            get { return (ConsumedDays + AvailableDays); }
        }

        #endregion
    }
}

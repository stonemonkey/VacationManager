using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective

namespace Vm.BusinessObjects.VacationRequests
{
    [Serializable]
    public partial class VacationStatus : BusinessBase<VacationStatus>
    {
        #region Private fields

        private int _totalNumber;
        private int _taken;
        private int _left;

        #endregion

        #region Property definitions

        public static PropertyInfo<int> TotalNumberProperty =
            RegisterProperty<int>(c => c.TotalNumber, RelationshipTypes.PrivateField);

        public static PropertyInfo<int> TakenProperty =
            RegisterProperty<int>(c => c.Taken, RelationshipTypes.PrivateField);

        public static PropertyInfo<int> LeftProperty =
            RegisterProperty<int>(c => c.Left, RelationshipTypes.PrivateField);

        #endregion

        #region Public properties

        public int TotalNumber
        {
            get { return GetProperty(TotalNumberProperty, _totalNumber); }
        }

        public int Taken
        {
            get { return GetProperty(TakenProperty, _taken); }
        }

        public int Left
        {
            get { return GetProperty(LeftProperty, _left); }
        }

        #endregion
    }
}

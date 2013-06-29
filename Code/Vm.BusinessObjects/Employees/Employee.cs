using System;
using Csla;
using Csla.Serialization;
using VacationManager.Common.DataContracts;

namespace Vm.BusinessObjects.Employees
{
    [Serializable]
    public partial class Employee : BusinessBase<Employee>
    {
        #region Property definitions

        public static PropertyInfo<long> IdProperty =
            RegisterProperty<long>(c => c.Id);

        public static PropertyInfo<string> SurnameProperty =
            RegisterProperty<string>(c => c.Surname);

        public static PropertyInfo<string> FirstNameProperty =
            RegisterProperty<string>(c => c.Firstname);

        public static PropertyInfo<EmployeeRoles> RolesProperty =
            RegisterProperty<EmployeeRoles>(c => c.Roles);

        #endregion

        #region Public properties

        public long Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public string Surname
        {
            get { return GetProperty(SurnameProperty); }
            set { SetProperty(SurnameProperty, value); }
        }

        public string Firstname
        {
            get { return GetProperty(FirstNameProperty); }
            set { SetProperty(FirstNameProperty, value); }
        }

        public EmployeeRoles Roles
        {
            get { return GetProperty(RolesProperty); }
            set { SetProperty(RolesProperty, value); }
        }

        #endregion
    }
}

using System;
using Csla;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;

namespace VacationManager.Ui.BusinessObjects
{
    [Serializable]
    public class Employee : BusinessBase<Employee>
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

        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(long id)
        {
            using (var proxy = new ServiceProxy<IEmployeeService>(Configuration.ServiceAddress))
            {
                var serviceObject = proxy.GetChannel().GetEmployeeById(id);

                Id = serviceObject.Id;
                Surname = serviceObject.Surname;
                Firstname = serviceObject.Firstname;
                Roles = serviceObject.Roles;
            }
        }

        #endregion
    }
}

using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective
using Common.Model;

namespace BusinessObjects.Employees
{
    [Serializable]
    public partial class Employee : BusinessBase<Employee>
    {
        private string _managerFullName;

        #region Property definitions

        private static PropertyInfo<long> IdProperty =
            RegisterProperty<long>(c => c.EmployeeId);

        private static PropertyInfo<string> LastNameProperty =
            RegisterProperty<string>(c => c.LastName);

        private static PropertyInfo<string> FirstNameProperty =
            RegisterProperty<string>(c => c.FirstName);

        private static PropertyInfo<EmployeeRoles> RolesProperty =
            RegisterProperty<EmployeeRoles>(c => c.Roles);

        private static PropertyInfo<string> CnpProperty =
            RegisterProperty<string>(c => c.Cnp);

        private static PropertyInfo<string> GenderProperty =
            RegisterProperty<string>(c => c.Gender);

        private static PropertyInfo<DateTime> BirthDateProperty =
            RegisterProperty<DateTime>(c => c.BirthDate);

        private static PropertyInfo<string> EmailProperty =
            RegisterProperty<string>(c => c.Email);

        private static PropertyInfo<string> AddressProperty =
            RegisterProperty<string>(c => c.Address);

        private static PropertyInfo<string> PhoneNumberProperty =
            RegisterProperty<string>(c => c.PhoneNumber);

        private static PropertyInfo<DateTime> HireDateProperty =
            RegisterProperty<DateTime>(c => c.HireDate);
        
        private static PropertyInfo<string> ManagerFullNameProperty =
            RegisterProperty<string>(c => c.ManagerFullName, RelationshipTypes.PrivateField);

        private static PropertyInfo<long> ManagerIdProperty =
            RegisterProperty<long>(c => c.ManagerId);

        #endregion

        #region Public properties

        public long EmployeeId
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            set { SetProperty(LastNameProperty, value); }
        }

        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            set { SetProperty(FirstNameProperty, value); }
        }

        public EmployeeRoles Roles
        {
            get { return GetProperty(RolesProperty); }
            set { SetProperty(RolesProperty, value); }
        }

        public string Cnp
        {
            get { return GetProperty(CnpProperty); }
            set { SetProperty(CnpProperty, value); }
        }

        public string Gender
        {
            get { return GetProperty(GenderProperty); }
            set { SetProperty(GenderProperty, value); }
        }

        public DateTime BirthDate
        {
            get { return GetProperty(BirthDateProperty); }
            set { SetProperty(BirthDateProperty, value); }
        }

        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }

        public string Address
        {
            get { return GetProperty(AddressProperty); }
            set { SetProperty(AddressProperty, value); }
        }

        public string PhoneNumber
        {
            get { return GetProperty(PhoneNumberProperty); }
            set { SetProperty(PhoneNumberProperty, value); }
        }

        public DateTime HireDate
        {
            get { return GetProperty(HireDateProperty); }
            set { SetProperty(HireDateProperty, value); }
        }
        
        public string ManagerFullName
        {
            get { return GetProperty(ManagerFullNameProperty, _managerFullName); }
        }

        public long ManagerId
        {
            get { return GetProperty(ManagerIdProperty); }
            set { SetProperty(ManagerIdProperty, value); }
        }

        #endregion
    }
}

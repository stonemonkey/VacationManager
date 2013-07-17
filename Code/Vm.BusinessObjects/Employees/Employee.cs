using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective
using VacationManager.Common.Model;

namespace Vm.BusinessObjects.Employees
{
    [Serializable]
    public partial class Employee : BusinessBase<Employee>
    {
        #region Property definitions

        public static PropertyInfo<long> IdProperty =
            RegisterProperty<long>(c => c.Id);

        public static PropertyInfo<string> LastNameProperty =
            RegisterProperty<string>(c => c.LastName);

        public static PropertyInfo<string> FirstNameProperty =
            RegisterProperty<string>(c => c.Firstname);

        public static PropertyInfo<EmployeeRoles> RolesProperty =
            RegisterProperty<EmployeeRoles>(c => c.Roles);

        public static PropertyInfo<string> CnpProperty =
            RegisterProperty<string>(c => c.Cnp);
        
        public static PropertyInfo<string> GenderProperty =
            RegisterProperty<string>(c => c.Gender);

        public static PropertyInfo<DateTime> BirthDateProperty =
            RegisterProperty<DateTime>(c => c.BirthDate);

        public static PropertyInfo<string> EmailProperty =
            RegisterProperty<string>(c => c.Email);

        public static PropertyInfo<string> AddressProperty =
            RegisterProperty<string>(c => c.Address);

        public static PropertyInfo<string> PhoneNumberProperty =
            RegisterProperty<string>(c => c.PhoneNumber);
        
        public static PropertyInfo<DateTime> HireDateProperty =
            RegisterProperty<DateTime>(c => c.HireDate);
        
        public static PropertyInfo<int> AvailableNumberOfDaysProperty =
            RegisterProperty<int>(c => c.AvailableNumberOfDays);

        #endregion

        #region Public properties

        public long Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            set { SetProperty(LastNameProperty, value); }
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

        public int AvailableNumberOfDays
        {
            get { return GetProperty(AvailableNumberOfDaysProperty); }
            set { SetProperty(AvailableNumberOfDaysProperty, value); }
        }

        #endregion
    }
}

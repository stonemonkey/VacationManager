using Persistence;

namespace BusinessObjects.Employees
{
    public partial class Employee
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Create(Persistence.Model.EmployeeEntity entity)
        {
            EmployeeId = entity.Id;
            FirstName = entity.Firstname;
            LastName = entity.LastName;

            PhoneNumber = entity.PhoneNumber;
            Address = entity.Address;
            Email = entity.Email;

            Cnp = entity.Cnp;
            BirthDate = entity.BirthDate;
            HireDate = entity.HireDate;

            Roles = entity.Roles;

            if (entity.Manager != null)
                _managerFullName = entity.Manager.Firstname + " " + entity.Manager.LastName;
        }

        protected void DataPortal_Fetch(long id)
        {
            using (var ctx = new VacationManagerContext())
            {
                var employee = ctx.Employees.Find(id);

                EmployeeId = employee.Id;
                LastName = employee.LastName;
                FirstName = employee.Firstname;
                Roles = employee.Roles;
            }
        }

        #endregion
    }
}

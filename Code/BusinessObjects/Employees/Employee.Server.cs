using Persistence;

namespace BusinessObjects.Employees
{
    public partial class Employee
    {
        #region DataPortal_XYZ methods

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

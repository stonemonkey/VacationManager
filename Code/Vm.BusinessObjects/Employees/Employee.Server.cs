using VacationManager.Persistence;

namespace Vm.BusinessObjects.Employees
{
    public partial class Employee
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(long id)
        {
            using (var ctx = new VacationManagerContext())
            {
                var employee = ctx.Employees.Find(id);

                Id = employee.Id;
                LastName = employee.LastName;
                Firstname = employee.Firstname;
                Roles = employee.Roles;
            }
        }

        #endregion
    }
}

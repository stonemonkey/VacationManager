using System;
using System.Diagnostics;
using VacationManager.Persistence;

namespace Vm.BusinessObjects.Employees
{
    public partial class Employee
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(long id)
        {
            try
            {
                using (var ctx = new VacationManagerContext())
                {
                    var employee = ctx.Employees.Find(id);

                    Id = employee.Id;
                    Surname = employee.Surname;
                    Firstname = employee.Firstname;
                    Roles = employee.Roles;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion
    }
}

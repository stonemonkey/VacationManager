using System.Collections.Generic;
using System.Linq;
using Csla.Core;
using Csla.Security;
using Common.Model;
using Persistence;
using Persistence.Model;

namespace BusinessObjects.Security
{
    public partial class VmIdentity
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(UsernameCriteria criteria)
        {
            using (var ctx = new VacationManagerContext())
            {
                var employee = ctx.Employees
                    .SingleOrDefault(x => x.Email == criteria.Username);

                LoadIdentityFrom(employee);
            }
        }

        #endregion

        #region Private methods

        private void LoadIdentityFrom(EmployeeEntity employee)
        {
            if (employee != null)
            {
                _employeeid = employee.Id;
                Name = employee.Email;
                IsAuthenticated = true;
                AuthenticationType = "Custom";
                Roles = new MobileList<string>(GetRoles(employee.Roles));
            }
            else
            {
                _employeeid = 0;
                Name = string.Empty;
                IsAuthenticated = false;
                AuthenticationType = string.Empty;
                Roles = new MobileList<string>();
            }
        }

        private static IEnumerable<string> GetRoles(EmployeeRoles roles)
        {
            if ((roles & EmployeeRoles.Executive) == EmployeeRoles.Executive)
                yield return EmployeeRoles.Executive.ToString();
            
            if ((roles & EmployeeRoles.Manager) == EmployeeRoles.Manager)
                yield return EmployeeRoles.Manager.ToString();
            
            if ((roles & EmployeeRoles.Hr) == EmployeeRoles.Hr)
                yield return EmployeeRoles.Hr.ToString();
        }

        #endregion
    }
}
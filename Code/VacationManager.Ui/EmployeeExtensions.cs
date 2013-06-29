using VacationManager.Common.Model;
using Vm.BusinessObjects.Employees;

namespace VacationManager.Ui
{
    public static class EmployeeExtensions
    {
        public static bool IsIn(this Employee employee, EmployeeRoles roles)
        {
            return (employee.Roles & roles) == roles;
        }
    }
}
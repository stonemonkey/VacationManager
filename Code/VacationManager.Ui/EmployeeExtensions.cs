using BusinessObjects.Employees;
using Common.Model;

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
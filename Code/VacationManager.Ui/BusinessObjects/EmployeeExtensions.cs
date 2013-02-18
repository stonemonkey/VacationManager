using VacationManager.Common.DataContracts;

namespace VacationManager.Ui.BusinessObjects
{
    public static class EmployeeExtensions
    {
        public static bool IsIn(this Employee employee, EmployeeRoles roles)
        {
            return (employee.Roles & roles) == roles;
        }
    }
}
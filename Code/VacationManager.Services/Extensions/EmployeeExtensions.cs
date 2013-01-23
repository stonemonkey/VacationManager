using VacationManager.Common.DataContracts;
using VacationManager.Persistence.Model;

namespace VacationManager.Services.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto ToDto(this Employee employee)
        {
            if (employee == null)
                return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                Roles = employee.Roles,
                Surname = employee.Surname,
                Firstname = employee.Firstname,
                EmailAddress = employee.EmailAddress,
                ManagerId = employee.Manager == null ? 0 : employee.Manager.Id,
            };
        }
    }
}
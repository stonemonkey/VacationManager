using System.ComponentModel.DataAnnotations;
using VacationManager.Common.DataContracts;

namespace VacationManager.Services.Model
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        
        public string Surname { get; set; }

        public string Firstname { get; set; }

        public string EmailAddress { get; set; }

        public EmployeeRoles Roles { get; set; }

        public virtual Employee Manager { get; set; }
    }

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

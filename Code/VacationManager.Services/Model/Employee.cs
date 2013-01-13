using System.ComponentModel.DataAnnotations;
using VacationManager.Common.DataContracts;

namespace VacationManager.Services.Model
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        
        //public Employee Manager { get; set; }

        public string Surname { get; set; }

        public string Firstname { get; set; }

        public string EmailAddress { get; set; }

        public EmployeeRoles Roles { get; set; }
    }
}

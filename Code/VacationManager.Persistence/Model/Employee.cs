using System;
using System.ComponentModel.DataAnnotations;
using VacationManager.Common.Model;

namespace VacationManager.Persistence.Model
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        
        public string LastName { get; set; }
        public string Firstname { get; set; }

        public string Cnp { get; set; }        
        public string Gender { get; set; }     
        public DateTime BirthDate { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }    
        public string PhoneNumber { get; set; }
        
        public DateTime HireDate { get; set; } 
        public EmployeeRoles Roles { get; set; }
        public virtual Employee Manager { get; set; }
    }
}

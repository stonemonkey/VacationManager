using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Model;

namespace Persistence.Model
{
    [Table("Employees")]
    public class EmployeeEntity
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
        public virtual EmployeeEntity Manager { get; set; }
        public virtual EmployeeSituationEntity Situation { get; set; }

        // TODO: check this http://crackstation.net/hashing-security.htm for how to fill this values
        public string Salt { get; set; }
        public string Password { get; set; }
    }
}

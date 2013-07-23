using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Model;

namespace Persistence.Model
{
    [Table("VacationRequests")]
    public class VacationRequestEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public VacationRequestState State { get; set; }
               
        public virtual EmployeeEntity Employee { get; set; }
    }
}

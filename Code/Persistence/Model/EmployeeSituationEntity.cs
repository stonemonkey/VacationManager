using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Model
{
    [Table("EmployeeSituations")]
    public class EmployeeSituationEntity
    {
        [Key]
        public long Id { get; set; }

        public int Year { get; set; }

        public int PaidDays { get; set; }

        public int ConsumedDays { get; set; }

        public int AvailableDays { get; set; }

        public virtual EmployeeEntity Employee { get; set; }
    }
}
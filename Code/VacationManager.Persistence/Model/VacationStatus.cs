using System.ComponentModel.DataAnnotations;

namespace VacationManager.Persistence.Model
{
    public class VacationStatus
    {
        [Key]
        public long Id { get; set; }

        public int Year { get; set; }

        public int Paid { get; set; }

        public int Left { get; set; }

        public int Taken { get; set; }

        public int TotalNumber { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
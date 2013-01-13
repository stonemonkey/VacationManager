using System.ComponentModel.DataAnnotations;

namespace VacationManager.Services.Model
{
    public class VacationDays
    {
        [Key]
        public long Id { get; set; }

        public Employee Employee { get; set; }

        public int Year { get; set; }

        public int TotalNumber { get; set; }

        public int Taken { get; set; }

        public int Left { get; set; }

        public int Paid { get; set; }
    }
}
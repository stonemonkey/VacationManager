using System.ComponentModel.DataAnnotations;
using VacationManager.Common.DataContracts;

namespace VacationManager.Services.Model
{
    public class VacationDays
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

    public static class VacationDaysExtensions
    {
        public static VacationDaysDto ToDto(this VacationDays vacationDays)
        {
            if (vacationDays == null)
                return null;

            return new VacationDaysDto
            {
                Year = vacationDays.Year,
                Paid = vacationDays.Paid,
                Left = vacationDays.Left,
                Taken = vacationDays.Taken,
                TotalNumber = vacationDays.TotalNumber,
                EmployeeId = vacationDays.Employee.Id,
            };
        }
    }
}
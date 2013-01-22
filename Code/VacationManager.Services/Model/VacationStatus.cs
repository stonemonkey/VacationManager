using System.ComponentModel.DataAnnotations;
using VacationManager.Common.DataContracts;

namespace VacationManager.Services.Model
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

    public static class VacationStatusExtensions
    {
        public static VacationStatusDto ToDto(this VacationStatus vacationStatus)
        {
            if (vacationStatus == null)
                return null;

            return new VacationStatusDto
            {
                Year = vacationStatus.Year,
                Paid = vacationStatus.Paid,
                Left = vacationStatus.Left,
                Taken = vacationStatus.Taken,
                TotalNumber = vacationStatus.TotalNumber,
                EmployeeId = vacationStatus.Employee.Id,
            };
        }
    }
}
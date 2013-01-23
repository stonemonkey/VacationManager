using VacationManager.Common.DataContracts;
using VacationManager.Persistence.Model;

namespace VacationManager.Services.Extensions
{
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
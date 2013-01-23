using System;
using System.Text;
using VacationManager.Common.DataContracts;
using VacationManager.Persistence.Model;

namespace VacationManager.Services.Extensions
{
    public static class VacationRequestDtoExtensions
    {
        public static VacationRequest ToModel(this VacationRequestDto vacationRequestDto, Employee employee)
        {
            if (vacationRequestDto == null)
                return null;

            var vacationDaysBuilder = new StringBuilder();
            vacationRequestDto.VacationDays.ForEach(x => vacationDaysBuilder.Append(
                x.ToString(VacationRequest.VacationDaysFormat) + VacationRequest.VacationDaysSeparator));

            return new VacationRequest
            {
                Employee = employee,
                CreationDate = DateTime.Now,
                State = VacationRequestState.Submitted,
                VacationDays = vacationDaysBuilder.ToString(),
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using VacationManager.Common.DataContracts;
using VacationManager.Persistence.Model;

namespace VacationManager.Services.Extensions
{
    public static class VacationRequestExtensions
    {
        public static VacationRequestDto ToDto(this VacationRequest vacationRequest)
        {
            if (vacationRequest == null)
                return null;

            // TODO: check if DateTime.Parse may introduce errors because of culture
            var vacationDays = vacationRequest.VacationDays
                .Split(new[] {VacationRequest.VacationDaysSeparator}, StringSplitOptions.RemoveEmptyEntries).ToList()
                .ConvertAll(DateTime.Parse);

            return new VacationRequestDto
            {
                VacationDays = vacationDays,
                State = vacationRequest.State,
                EmployeeId = vacationRequest.Employee.Id,
                CreationDate = vacationRequest.CreationDate,
                RequestNumber = vacationRequest.RequestNumber,
                EmployeeFullName = vacationRequest.Employee.Firstname + " " + vacationRequest.Employee.Surname,
            };
        }

        public static List<VacationRequestDto> ToDtos(this List<VacationRequest> vacationRequests)
        {
            if (vacationRequests == null)
                return null;

            return vacationRequests.ConvertAll(x => x.ToDto());
        }
    }
}
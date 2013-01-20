using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VacationManager.Common.DataContracts;

namespace VacationManager.Services.Model
{
    public class VacationRequest
    {
        [Key]
        public long RequestNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public VacationRequestState State { get; set; }
        
        public string VacationDays { get; set; }
        
        public virtual Employee Employee { get; set; }
    }

    public static class VacationRequestExtensions
    {
        public const string VacationDaysFormat = "yyyy-MM-dd";
        public const char VacationDaysSeparator = ';';

        public static VacationRequestDto ToDto(this VacationRequest vacationRequest)
        {
            if (vacationRequest == null)
                return null;

            // TODO: check if DateTime.Parse may introduce errors because of culture
            var vacationDays = vacationRequest.VacationDays
                .Split(new[] { VacationDaysSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList()
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

        public static VacationRequest ToEntity(this VacationRequestDto vacationRequestDto, Employee employee)
        {
            if (vacationRequestDto == null)
                return null;

            var vacationDaysBuilder = new StringBuilder();
            vacationRequestDto.VacationDays
                .ForEach(vd => vacationDaysBuilder.Append(vd.ToString(VacationDaysFormat) + VacationDaysSeparator));

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

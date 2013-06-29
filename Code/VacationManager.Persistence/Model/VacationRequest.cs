using System;
using System.ComponentModel.DataAnnotations;
using VacationManager.Common.Model;

namespace VacationManager.Persistence.Model
{
    public class VacationRequest
    {
        public const string VacationDaysFormat = "yyyy-MM-dd";
        public const char VacationDaysSeparator = ';';

        [Key]
        public long RequestNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public VacationRequestState State { get; set; }
        
        public string VacationDays { get; set; }
        
        public virtual Employee Employee { get; set; }
    }
}

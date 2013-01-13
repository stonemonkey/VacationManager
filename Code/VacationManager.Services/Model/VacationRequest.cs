using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacationManager.Common.DataContracts;

namespace VacationManager.Services.Model
{
    public class VacationRequest
    {
        [Key]
        public long RequestNumber { get; set; }

        public Employee Employee { get; set; }

        public DateTime CreationDate { get; set; }

        //public List<DateTime> VacationDays { get; set; }

        public VacationRequestState State { get; set; }
    }
}

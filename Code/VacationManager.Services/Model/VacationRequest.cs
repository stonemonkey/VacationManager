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

        public DateTime CreationDate { get; set; }

        public VacationRequestState State { get; set; }
        
        // TODO: have to persist this too ...
        public List<DateTime> VacationDays { get; set; }
        
        public virtual Employee Employee { get; set; }
    }
}

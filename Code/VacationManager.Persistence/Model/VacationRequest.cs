using System;
using System.ComponentModel.DataAnnotations;
using VacationManager.Common.Model;

namespace VacationManager.Persistence.Model
{
    public class VacationRequest
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public VacationRequestState State { get; set; }
               
        public virtual Employee Employee { get; set; }
    }
}

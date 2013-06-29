using System;

namespace VacationManager.Common.Model
{
    [Serializable]
    public class VacationRequestSearchCriteria
    {
        public bool GetMine { get; set; }
        
        public long EmployeeId { get; set; }

        public VacationRequestState[] States { get; set; }
    }
}

using System.Runtime.Serialization;

namespace VacationManager.Common.DataContracts
{
    [DataContract]
    public class VacationRequestSearchCriteriaDto
    {
        [DataMember]
        public bool GetMine { get; set; }
        
        [DataMember]
        public long EmployeeId { get; set; }

        [DataMember]
        public VacationRequestState[] States { get; set; }
    }
}

using System.Runtime.Serialization;

namespace VacationManager.Common.DataContracts
{
    [DataContract]
    public class VacationStatusDto
    {
        [DataMember]
        public long EmployeeId { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int TotalNumber { get; set; }

        [DataMember]
        public int Taken { get; set; }

        [DataMember]
        public int Left { get; set; }

        [DataMember]
        public int Paid { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VacationManager.Common.DataContracts
{
    [DataContract]
    public class VacationRequestDto
    {
        public VacationRequestDto()
        {
            VacationDays = new List<DateTime>();
        }

        [DataMember]
        public long RequestNumber { get; set; }

        [DataMember]
        public VacationRequestState State { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public List<DateTime> VacationDays { get; set; }

        [DataMember]
        public long EmployeeId { get; set; }

        [DataMember]
        public string EmployeeFullName { get; set; }
    }
}

using System.Runtime.Serialization;

namespace VacationManager.Common.DataContracts
{
    [DataContract]
    public class EmployeeDto
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public long ManagerId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string SurnameName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public EmployeeRoles Roles { get; set; }
    }
}

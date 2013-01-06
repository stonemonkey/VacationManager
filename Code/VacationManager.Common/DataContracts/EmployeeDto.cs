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
        public string Surname { get; set; }

        [DataMember]
        public string Firstname { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public EmployeeRoles Roles { get; set; }
    }
}

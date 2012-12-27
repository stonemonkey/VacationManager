using System;
using System.Runtime.Serialization;

namespace VacationManager.Common.DataContracts
{
    [DataContract]
    [Flags]
    public enum EmployeeRoles
    {
        [EnumMember]
        Executive,

        [EnumMember]
        Manager,
        
        [EnumMember]
        Hr,
    }
}
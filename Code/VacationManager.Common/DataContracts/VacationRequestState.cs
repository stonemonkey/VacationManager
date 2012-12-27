using System.Runtime.Serialization;

namespace VacationManager.Common.DataContracts
{
    [DataContract]
    public enum VacationRequestState
    {
        [EnumMember]
        Submitted = 1,
        
        [EnumMember]
        Approved = 2,
        
        [EnumMember]
        Rejected = 3,
    }
}
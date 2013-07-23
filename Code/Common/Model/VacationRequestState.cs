using System;

namespace Common.Model
{
    [Serializable]
    public enum VacationRequestState
    {
        Submitted = 1,
        
        Approved = 2,
        
        Rejected = 3,

        Processed = 4,
    }
}
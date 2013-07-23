using System;

namespace Common.Model
{
    [Serializable]
    [Flags]
    public enum EmployeeRoles
    {
        Executive,

        Manager,
        
        Hr,
    }
}
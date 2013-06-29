using System;

namespace VacationManager.Common.Model
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
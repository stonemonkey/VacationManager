using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective

namespace BusinessObjects.Employees
{
    [Serializable]
    public partial class EmployeeInfoList : ReadOnlyListBase<EmployeeInfoList, Employee>
    {
    }
}
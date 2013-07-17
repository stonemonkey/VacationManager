using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective

namespace Vm.BusinessObjects.Employees
{
    [Serializable]
    public class EmployeeInfoList : ReadOnlyListBase<EmployeeInfoList, Employee>
    {
    }
}
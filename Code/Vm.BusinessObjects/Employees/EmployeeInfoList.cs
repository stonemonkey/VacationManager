using System;
using Csla;
using Csla.Serialization;

namespace Vm.BusinessObjects.Employees
{
    [Serializable]
    public class EmployeeInfoList : ReadOnlyListBase<EmployeeInfoList, Employee>
    {
    }
}
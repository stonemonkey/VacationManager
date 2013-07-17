using System;
using Csla;
using Csla.Security;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective

namespace Vm.BusinessObjects.Security
{
    [Serializable]
    public partial class VmIdentity : CslaIdentity
    {
        private long _employeeid = EmployeeIdProperty.DefaultValue;

        private static PropertyInfo<long> EmployeeIdProperty = RegisterProperty(
            typeof(VmIdentity), new PropertyInfo<long>("EmployeeId", RelationshipTypes.PrivateField));
        
        public long EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty, _employeeid); }
        }
    }
}
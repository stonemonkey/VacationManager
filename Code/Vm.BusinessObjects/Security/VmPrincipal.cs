using System;
using System.Security.Principal;
using Csla.Security;

namespace Vm.BusinessObjects.Security
{
    [Serializable]
    public class VmPrincipal : CslaPrincipal
    {
        public VmPrincipal(IIdentity identity)
            : base(identity)
        { }
    }
}
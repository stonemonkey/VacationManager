using System;
using Csla;
using Csla.Serialization;

namespace Vm.BusinessObjects.VacationRequests
{
    [Serializable]
    public partial class VacationRequestInfoList : ReadOnlyListBase<VacationRequestInfoList, VacationRequest>
    {
    }
}
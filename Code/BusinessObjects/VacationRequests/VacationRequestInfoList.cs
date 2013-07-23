using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective

namespace BusinessObjects.VacationRequests
{
    [Serializable]
    public partial class VacationRequestInfoList : 
        ReadOnlyListBase<VacationRequestInfoList, VacationRequest>
    {
    }
}
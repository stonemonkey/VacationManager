using Csla;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;
using Vm.BusinessObjects.Server;

namespace Vm.BusinessObjects.VacationRequests
{
    public partial class VacationRequestInfoList
    {
        protected void DataPortal_Fetch(VacationRequestSearchCriteriaDto criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (var proxy = new ServiceProxy<IVacationRequestService>(Configuration.ServiceAddress))
            {
                var channel = proxy.GetChannel();
                var requests = channel.SearchRequests(criteria);

                foreach (var item in requests)
                    Add(DataPortal.Create<VacationRequest>(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
    }
}
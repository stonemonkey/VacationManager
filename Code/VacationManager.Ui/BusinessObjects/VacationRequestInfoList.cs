using System.Collections.Generic;
using System.ServiceModel;
using Csla;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;

namespace VacationManager.Ui.BusinessObjects
{
    public class VacationRequestInfoList : ReadOnlyListBase<VacationRequestInfoList, VacationRequest>
    {
        private void DataPortal_Fetch(bool myRequests)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (var proxy = new ServiceProxy<IVacationRequestService>(Configuration.ServiceAddress))
            {
                var channel = proxy.GetChannel();
                
                IList<VacationRequestDto> requests;
                if (myRequests)
                    requests = channel.GetMyRequests();
                else
                    requests = channel.GetPendingRequests();

                foreach (var item in requests)
                    Add(DataPortal.Create<VacationRequest>(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
    }
}
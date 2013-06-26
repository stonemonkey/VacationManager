using VacationManager.Common.ServiceContracts;
using Vm.BusinessObjects.Server;

namespace Vm.BusinessObjects.VacationRequests
{
    public partial class ChangeVacationRequestStateCommand
    {
        #region DataPortal_XYZ methods

        protected override void DataPortal_Execute()
        {
            using (var proxy = new ServiceProxy<IVacationRequestService>(Configuration.ServiceAddress))
            {
                proxy.GetChannel().ChangeRequestState(_requestNumber, _state);
            }
        }

        #endregion
    }
}

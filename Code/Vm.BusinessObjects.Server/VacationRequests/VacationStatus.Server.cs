using VacationManager.Common.ServiceContracts;
using Vm.BusinessObjects.Server;

namespace Vm.BusinessObjects.VacationRequests
{
    public partial class VacationStatus
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(long employeeId)
        {
            using (var proxy = new ServiceProxy<IVacationStatusService>(Configuration.ServiceAddress))
            {
                var createdServiceObject = proxy.GetChannel().GetVacationStatusByEmployeeId(employeeId);

                _totalNumber = createdServiceObject.TotalNumber;
                _taken = createdServiceObject.Taken;
                _left = createdServiceObject.Left;
            }
        }

        #endregion
    }
}

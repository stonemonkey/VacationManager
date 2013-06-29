using System.Linq;
using VacationManager.Persistence;

namespace Vm.BusinessObjects.VacationRequests
{
    public partial class VacationStatus
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(long employeeId)
        {
            using (var ctx = new VacationManagerContext())
            {
                var vacationDays = ctx.VacationStatus.FirstOrDefault(x => x.Employee.Id == employeeId);

                _totalNumber = vacationDays.TotalNumber;
                _taken = vacationDays.Taken;
                _left = vacationDays.Left;
            }
        }

        #endregion
    }
}

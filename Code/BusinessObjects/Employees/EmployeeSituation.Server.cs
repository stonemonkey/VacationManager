using System.Linq;
using Persistence;

namespace BusinessObjects.Employees
{
    public partial class EmployeeSituation
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(long employeeId)
        {
            using (var ctx = new VacationManagerContext())
            {
                var vacationDays = ctx.Situations
                    .FirstOrDefault(x => x.Employee.Id == employeeId);

                // TODO: what if vacationDays is null?

                _consumedDays = vacationDays.ConsumedDays;
                _availableDays = vacationDays.AvailableDays;
            }
        }

        #endregion
    }
}

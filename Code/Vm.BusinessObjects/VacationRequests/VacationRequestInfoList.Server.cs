using System.Collections.Generic;
using System.Linq;
using Csla;
using VacationManager.Common.Model;
using VacationManager.Persistence;

namespace Vm.BusinessObjects.VacationRequests
{
    public partial class VacationRequestInfoList
    {
        protected void DataPortal_Fetch(VacationRequestSearchCriteria criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            IList<VacationManager.Persistence.Model.VacationRequest> requests;

            using (var ctx = new VacationManagerContext())
            {
                if (criteria == null)
                    requests = ctx.Requests.ToList();
                else
                {
                    var query = ctx.Requests.AsQueryable();
                    if (criteria.GetMine)
                        query = query.Where(x => x.Employee.Id == criteria.EmployeeId);
                    else
                        query = query.Where(x => x.Employee.Manager.Id == criteria.EmployeeId);

                    if (criteria.States != null)
                        query = query.Where(x => criteria.States.Contains(x.State));

                    requests = query.OrderByDescending(x => x.CreationDate).ToList();
                }
            }

            foreach (var item in requests)
                Add(DataPortal.Create<VacationRequest>(item));

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
    }
}
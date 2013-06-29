using System;
using System.Linq;
using VacationManager.Common.Model;
using VacationManager.Persistence;

namespace Vm.BusinessObjects.VacationRequests
{
    public partial class ChangeVacationRequestStateCommand
    {
        #region DataPortal_XYZ methods

        protected override void DataPortal_Execute()
        {
            using (var ctx = new VacationManagerContext())
            {
                var request = ctx.Requests.FirstOrDefault(x => x.RequestNumber == _requestNumber);
                if (request == null)
                    throw new ApplicationException(string.Format(
                        "Request number {0} was not found. It must exist in order to change it's state.", _requestNumber));

                if (request.State != VacationRequestState.Submitted)
                    throw new ApplicationException(string.Format(
                        "Request {0} was already {1}, cannot change it's state anymore. It must be in submited state in order to change it's state.",
                        _requestNumber, request.State));

                request.State = _state;
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}

using System;
using System.Linq;
using Persistence;

namespace BusinessObjects.VacationRequests
{
    public partial class ChangeVacationRequestStateCommand
    {
        #region DataPortal_XYZ methods

        protected override void DataPortal_Execute()
        {
            using (var ctx = new VacationManagerContext())
            {
                var request = ctx.Requests.FirstOrDefault(x => x.Id == _requestNumber);
                if (request == null)
                    throw new ApplicationException(string.Format(
                        "Request number {0} was not found. It must exist in order to change it's state.", _requestNumber));

                request.State = _state;
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}

using System;
using Csla;
using VacationManager.Common.DataContracts;
using VacationManager.Common.ServiceContracts;

namespace VacationManager.Ui.BusinessObjects
{
    [Serializable]
    public class ChangeVacationRequestStateCommand : CommandBase<ChangeVacationRequestStateCommand>
    {
        #region Private fields

        private readonly long _requestNumber;
        private readonly VacationRequestState _state;

        #endregion

        public ChangeVacationRequestStateCommand(long requestNumber, VacationRequestState toState)
        {
            _state = toState;
            _requestNumber = requestNumber;
        }

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

using System;
using Csla;
using Csla.Serialization;
using VacationManager.Common.DataContracts;

namespace Vm.BusinessObjects.VacationRequests
{
    [Serializable]
    public partial class ChangeVacationRequestStateCommand : CommandBase<ChangeVacationRequestStateCommand>
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
    }
}

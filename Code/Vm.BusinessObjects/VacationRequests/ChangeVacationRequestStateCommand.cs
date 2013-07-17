using System;
using Csla;
// ReSharper disable RedundantUsingDirective
using Csla.Serialization;
// ReSharper restore RedundantUsingDirective
using VacationManager.Common.Model;

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

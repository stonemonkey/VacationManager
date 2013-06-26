using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Common.DataContracts;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;
using Vm.BusinessObjects.VacationRequests;

namespace VacationManager.Ui.Components.PendingRequests
{
    // TODO: extract common RequestsViewModelBase class see ApprovedRequestsViewModel and VacationRequestsViewModel
    public class PendingRequestsViewModel : Screen, IPopulableViewModel
    {
        #region Private fields

        private VacationRequestInfoList _items;
        private VacationRequest _selectedItem;

        #endregion

        public static PendingRequestsStrings Localization
        {
            get
            {
                return new PendingRequestsStrings();
            }
        }

        #region External dependencies

        [Inject]
        public IUiService UiService { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IContextViewModel Context { get; set; }

        #endregion

        #region Binding properties
        
        public VacationRequestInfoList Items
        {
            get { return _items; }
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public VacationRequest SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
                NotifyOfPropertyChange(() => CanApproveRequest);
                NotifyOfPropertyChange(() => CanRejectRequest);
            }
        }

        public bool CanApproveRequest
        {
            get { return CanChangeSelectedItemState(); }
        }

        public bool CanRejectRequest
        {
            get { return CanChangeSelectedItemState(); }
        }

        #endregion

        public PendingRequestsViewModel()
        {
            DisplayName = PendingRequestsStrings.Title;
        }

        #region Actions

        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var criteria = new VacationRequestSearchCriteriaDto
            {
                GetMine = false, 
                EmployeeId = Context.CurrentEmployee.Id, 
                States = new[] { VacationRequestState.Submitted },
            };
            var result = DataService.FetchList<VacationRequestInfoList, VacationRequest>(criteria);
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                Items = result.Result;
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        public IEnumerable<IResult> ApproveRequest()
        {
            yield return new SequentialResult(
                ChangeSelecedItemState(VacationRequestState.Approved).GetEnumerator());
        }
        
        public IEnumerable<IResult> RejectRequest()
        {
            yield return new SequentialResult(
                ChangeSelecedItemState(VacationRequestState.Rejected).GetEnumerator());
        }

        #endregion

        private bool CanChangeSelectedItemState()
        {
            return _selectedItem != null;
        }

        private IEnumerable<IResult> ChangeSelecedItemState(VacationRequestState toState)
        {
            yield return UiService.ShowBusy();

            var result = DataService.Execute(
                new ChangeVacationRequestStateCommand(SelectedItem.RequestNumber, toState));
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                yield return new SequentialResult(Populate().GetEnumerator());
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }
    }
}
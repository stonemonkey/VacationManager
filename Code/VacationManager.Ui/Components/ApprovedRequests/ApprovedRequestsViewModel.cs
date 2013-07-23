using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using Common.Model;
using VacationManager.Ui.Components.PendingRequests;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;
using BusinessObjects.VacationRequests;

namespace VacationManager.Ui.Components.ApprovedRequests
{
    // TODO: extract common RequestsViewModelBase class see PendingRequestsViewModel and VacationRequestsViewModel
    public class ApprovedRequestsViewModel : Screen, IPopulableViewModel
    {
        private VacationRequestInfoList _items;
        private VacationRequest _selectedItem;

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
                NotifyOfPropertyChange(() => CanProcessRequest);
            }
        }

        public bool CanProcessRequest
        {
            get { return CanChangeSelectedItemState(); }
        }

        #endregion

        public ApprovedRequestsViewModel()
        {
            DisplayName = ApprovedRequestsStrings.Title;
        }

        #region Actions

        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var criteria = new VacationRequestSearchCriteria
            {
                States = new[] { VacationRequestState.Approved },
            };
            var result = DataService.FetchList<VacationRequestInfoList, VacationRequest>(criteria);
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                Items = result.Result;
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        public IEnumerable<IResult> ProcessRequest()
        {
            yield return new SequentialResult(
                ChangeSelecedItemState(VacationRequestState.Processed).GetEnumerator());
        }

        #endregion
        
        private bool CanChangeSelectedItemState()
        {
            return (_selectedItem != null);
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
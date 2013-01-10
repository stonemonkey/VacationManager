using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Common.DataContracts;
using VacationManager.Ui.BusinessObjects;
using VacationManager.Ui.Components.PendingRequests;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.ApprovedRequests
{
    // TODO: extract common RequestsViewModelBase class see PendingRequestsViewModel and VacationRequestsViewModel
    public class ApprovedRequestsViewModel : Screen, IPopulableViewModel
    {
        private VacationRequestInfoList _items;

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

        #endregion

        public ApprovedRequestsViewModel()
        {
            DisplayName = ApprovedRequestsStrings.Title;
        }

        #region Actions

        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var criteria = new VacationRequestSearchCriteriaDto
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

        #endregion
    }
}
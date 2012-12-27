using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.BusinessObjects;
using VacationManager.Ui.Components.Shell;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.MyRequests
{
    public class MyRequestsViewModel : 
        Screen, IPopulableViewModel, IExplorerViewModel<VacationRequestInfoList, VacationRequest>
    {
        #region Private fields

        private VacationRequestInfoList _items;
        private VacationRequest _selectedItem;

        #endregion
        
        public static MyRequestsStrings Localization
        {
            get
            {
                return new MyRequestsStrings();
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
                NotifyOfPropertyChange(() => CanCancelRequest);
            }
        }

        public bool CanCancelRequest
        {
            get { return _selectedItem != null; }
        }

        #endregion

        public MyRequestsViewModel()
        {
            DisplayName = MyRequestsStrings.Title;
        }

        #region Actions

        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var result = DataService.FetchList<VacationRequestInfoList, VacationRequest>(true);
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                Items = result.Result;
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        public IEnumerable<IResult> OpenDetailsForCreatingRequest()
        {
            yield return UiService.ShowBusy();

            var result = DataService.Create<VacationRequest>();
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                yield return UiService.ShowChild<MyRequestDetailsViewModel>()
                                      .In<IShellViewModel>()
                                      .Configure(x => x.Item = result.Result);
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        public IEnumerable<IResult> CancelRequest()
        {
            yield return UiService.ShowBusy();

            var result = DataService.Delete(_selectedItem, x => x.RequestNumber);
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                yield return new SequentialResult(Populate().GetEnumerator());
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        #endregion
    }
}
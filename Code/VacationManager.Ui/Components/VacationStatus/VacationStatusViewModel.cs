using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;


namespace VacationManager.Ui.Components.VacationStatus
{
    public class VacationStatusViewModel : Screen, IPopulableViewModel
    {
        private Vm.BusinessObjects.VacationRequests.VacationStatus _item;

        public static VacationStatusStrings Localization
        {
            get
            {
                return new VacationStatusStrings();
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

        public Vm.BusinessObjects.VacationRequests.VacationStatus Item
        {
            get { return _item; }
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => Item);
            }
        }

        #endregion

        public VacationStatusViewModel()
        {
            DisplayName = VacationStatusStrings.Title;
        }

        #region Actions
        
        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var result = DataService.Fetch<Vm.BusinessObjects.VacationRequests.VacationStatus>(Context.CurrentEmployee.Id);
            yield return result;

            yield return UiService.HideBusy();

            if (result.Error == null)
                Item = result.Result;
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        #endregion
    }
}
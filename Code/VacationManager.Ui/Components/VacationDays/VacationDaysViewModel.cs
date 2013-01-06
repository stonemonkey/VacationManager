using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.VacationDays
{
    public class VacationDaysViewModel : Screen, IPopulableViewModel
    {
        private BusinessObjects.VacationDays _item;

        public static VacationDaysStrings Localization
        {
            get
            {
                return new VacationDaysStrings();
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

        public BusinessObjects.VacationDays Item
        {
            get { return _item; }
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => Item);
            }
        }

        #endregion

        public VacationDaysViewModel()
        {
            DisplayName = VacationDaysStrings.Title;
        }

        #region Actions
        
        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var result = DataService.Fetch<BusinessObjects.VacationDays>(Context.CurrentEmployee.Id);
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
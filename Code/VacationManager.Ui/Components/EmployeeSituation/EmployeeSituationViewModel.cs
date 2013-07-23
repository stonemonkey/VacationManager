using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.EmployeeSituation
{
    public class EmployeeSituationViewModel : Screen, IPopulableViewModel
    {
        private BusinessObjects.Employees.EmployeeSituation _item;

        public static EmployeeSituationStrings Localization
        {
            get
            {
                return new EmployeeSituationStrings();
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

        public BusinessObjects.Employees.EmployeeSituation Item
        {
            get { return _item; }
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => Item);
            }
        }

        #endregion

        public EmployeeSituationViewModel()
        {
            DisplayName = EmployeeSituationStrings.Title;
        }

        #region Actions
        
        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var result = DataService.Fetch<BusinessObjects.Employees.EmployeeSituation>
                (Context.CurrentEmployee.EmployeeId);
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
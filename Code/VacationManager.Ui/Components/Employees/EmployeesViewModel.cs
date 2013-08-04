using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;
using BusinessObjects.Employees;

namespace VacationManager.Ui.Components.Employees
{
    public class EmployeesViewModel : 
        Screen, IPopulableViewModel, IExplorerViewModel<EmployeeInfoList, Employee>
    {
        #region Private fields

        private EmployeeInfoList _items;
        private Employee _selectedItem;

        #endregion

        public static EmployeesStrings Localization
        {
            get
            {
                return new EmployeesStrings();
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

        public EmployeeInfoList Items
        {
            get { return _items; }
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public Employee SelectedItem
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

        public EmployeesViewModel()
        {
            DisplayName = EmployeesStrings.Title;
        }

        #region Actions

        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var result = DataService.FetchList<EmployeeInfoList, Employee>(null);
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                Items = result.Result;
            else
                yield return UiService.ShowWindowsMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        //public IEnumerable<IResult> OpenDetailsForCreatingEmployee()
        //{
        //    yield return UiService.ShowBusy();

        //    var result = DataService.Create<Employee>();
        //    yield return result;

        //    yield return UiService.HideBusy();

        //    if (ReferenceEquals(null, result.Error))
        //        yield return UiService.ShowChild<EmployeesViewModel>()
        //                              .In<IShellViewModel>()
        //                              .Configure(x => x.Item = result.Result);
        //    else
        //        yield return UiService.ShowWindowsMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        //}

        //public IEnumerable<IResult> DeleteEmployee()
        //{
        //    yield return UiService.ShowBusy();

        //    var result = DataService.Delete(_selectedItem, x => x.Id);
        //    yield return result;

        //    yield return UiService.HideBusy();

        //    if (ReferenceEquals(null, result.Error))
        //        yield return new SequentialResult(Populate().GetEnumerator());
        //    else
        //        yield return UiService.ShowWindowsMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        //}

        #endregion
    }
}
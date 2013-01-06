using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Common.DataContracts;
using VacationManager.Ui.BusinessObjects;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.Context
{
    public class ContextViewModel : Screen, IContextViewModel
    {
        #region Private fields

        private string _welcomeMessage;
        private Employee _currentEmployee;

        #endregion

        public static ContextStrings Localization
        {
            get
            {
                return new ContextStrings();
            }
        }

        #region External dependencies

        [Inject]
        public IUiService UiService { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        #endregion

        #region Binding properties

        public string WelcomeMessage
        {
            get { return _welcomeMessage; } 
            set 
            { 
                _welcomeMessage = value;
                NotifyOfPropertyChange(() => WelcomeMessage);
            }
        }

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; } 
            set
            {
                _currentEmployee = value;
                if (_currentEmployee != null)
                {
                    // TODO: this needs refactoring!
                    string message = string.Format("Hello {0} {1}. Roles:", _currentEmployee.Firstname, _currentEmployee.Surname);
                    if ((_currentEmployee.Roles & EmployeeRoles.Executive) == EmployeeRoles.Executive)
                        message += " " + EmployeeRoles.Executive.ToString();
                    if ((_currentEmployee.Roles & EmployeeRoles.Manager) == EmployeeRoles.Manager)
                        message += " " + EmployeeRoles.Manager.ToString();
                    if ((_currentEmployee.Roles & EmployeeRoles.Hr) == EmployeeRoles.Hr)
                        message += " " + EmployeeRoles.Hr.ToString();
                    WelcomeMessage = message;
                }
            }
        }

        #endregion

        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            // TODO: in future server should return employee determined by checking caller identity.
            // for the moment, in order to be able to test more easily we are specifying the id of 
            // the employee we consider to be the caller.
            var result = DataService.Fetch<Employee>(1);
            yield return result;

            yield return UiService.HideBusy();

            if (result.Error == null)
                CurrentEmployee = result.Result;
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }
    }
}
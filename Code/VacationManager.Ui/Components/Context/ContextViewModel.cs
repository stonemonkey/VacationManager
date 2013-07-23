using System.Collections.Generic;
using Caliburn.Micro;
using Csla;
using Ninject;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;
using BusinessObjects.Employees;
using BusinessObjects.Security;

namespace VacationManager.Ui.Components.Context
{
    public class ContextViewModel : Screen, IContextViewModel
    {
        #region Private fields

        private string _rolesMessage;
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

        public string RolesMessage
        {
            get { return _rolesMessage; } 
            set 
            { 
                _rolesMessage = value;
                NotifyOfPropertyChange(() => RolesMessage);
            }
        }

        public string WelcomeMessage
        {
            get { return _welcomeMessage; } 
            set 
            { 
                _welcomeMessage = value;
                NotifyOfPropertyChange(() => WelcomeMessage);
            }
        }

        #endregion

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; } 
            set
            {
                _currentEmployee = value;
                if (_currentEmployee != null)
                {
                    RolesMessage = string.Format(
                        ContextStrings.RolesMessageFormat, _currentEmployee.Roles);
                    WelcomeMessage = string.Format(
                        ContextStrings.WelcomeMessageFormat, _currentEmployee.FirstName, _currentEmployee.LastName);
                }
            }
        }

        public IEnumerable<IResult> Populate()
        {
            yield return UiService.ShowBusy();

            var vmIdentity = (VmIdentity) ApplicationContext.User.Identity;
            // TODO: what if the cast fails?
            var result = DataService.Fetch<Employee>(vmIdentity.EmployeeId);
            yield return result;

            yield return UiService.HideBusy();

            if (result.Error == null)
                CurrentEmployee = result.Result;
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }
    }
}
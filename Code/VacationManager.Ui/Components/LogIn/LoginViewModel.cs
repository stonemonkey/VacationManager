using System.Collections.Generic;
using Caliburn.Micro;
using Csla;
using Csla.Security;
using Ninject;
using VacationManager.Ui.Components.Shell;
using VacationManager.Ui.Services;
using Vm.BusinessObjects.Security;

namespace VacationManager.Ui.Components.LogIn
{
    public class LoginViewModel : Screen 
    {
        private string _user;
        private string _password;
        private string _loginMessage;

        public LoginStrings Localization
        {
            get
            {
                return new LoginStrings();
            }
        }
        
        #region External dependencies
        
        [Inject]
        public IUiService UiService { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IShellViewModel ShellViewModel { get; set; }

        #endregion

        #region Binding properties

        public string User
        {
            get { return _user; }
            set
            {
                _user = value;

                NotifyOfPropertyChange(() => User);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;

                NotifyOfPropertyChange(() => Password);
            }
        }

        public string LoginMessage
        {
            get { return _loginMessage; }
            set
            {
                _loginMessage = value;

                NotifyOfPropertyChange(() => LoginMessage);
            }
        }

        #endregion

        public LoginViewModel()
        {
            DisplayName = LoginStrings.Title;
        }

        public IEnumerable<IResult> Login()
        {
            yield return UiService.ShowBusy();

            var result = DataService.Login(User, Password);
            yield return result;

            yield return UiService.HideBusy();

            if (result.Error == null)
            {
                if (result.Result == null || !result.Result.IsAuthenticated)
                {
                    ApplicationContext.User = new UnauthenticatedPrincipal();

                    LoginMessage = LoginStrings.InvalidCredentialsMessage;
                }
                else
                {
                    ApplicationContext.User = new VmPrincipal(result.Result);

                    yield return new SequentialResult(
                        ShellViewModel.Load().GetEnumerator());

                    if (Parent != null)
                    {
                        TryClose();
                    }
                }
            }
            else   
            {
                yield return UiService.ShowMessageBox(
                    result.Error.Message, LoginStrings.ErrorMessageBoxCaption);
            }        
        }
    }
}

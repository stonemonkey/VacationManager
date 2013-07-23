using System;
using System.Security.Principal;
using Caliburn.Micro;
using Csla;
using Csla.Security;
using BusinessObjects.Security;

namespace VacationManager.Ui.Results
{
    public class LoginResult : ResultBase<IIdentity>
    {
        private readonly string _user;
        private readonly string _password;

        public LoginResult(string user, string password)
        {
            _user = user;
            _password = password;
        }

        public override async void Execute(ActionExecutionContext context = null)
        {
            try
            {
                var identity = await DataPortal.FetchAsync<VmIdentity>(
                    new UsernameCriteria(_user, _password));

                Result = identity;
            }
            catch (Exception e)
            {
                Error = e;
            }

            InvokeCompleted(new ResultCompletionEventArgs());
        }
    }
}
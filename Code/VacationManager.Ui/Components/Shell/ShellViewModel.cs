using System.Collections.Generic;
using Caliburn.Micro;
using Csla;
using Ninject;
using Common.Model;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Components.Dashboard;
using VacationManager.Ui.Components.DialogBox;
using VacationManager.Ui.Components.Employees;
using VacationManager.Ui.Components.LogIn;
using VacationManager.Ui.Components.MenuBar;
using VacationManager.Ui.Components.MyRequests;
using VacationManager.Ui.Infrastructure;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.Shell
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {
        #region External dependencies
        
        [Inject]
        public IDialogBoxViewModel DialogBox { get; set; }
        
        [Inject]
        public IMenuBarViewModel MenuBar { get; set; }

        [Inject]
        public IContextViewModel Context { get; set; }

        [Inject]
        public IUiService UiService { get; set; }

        #endregion

        public ShellViewModel()
        {
            DisplayName = ShellStrings.AppName;
        }

        public void CloseItem(IScreen screen)
        {
            screen.TryClose();
        }

        public IEnumerable<IResult> Load()
        {
            // load context
            yield return new SequentialResult(Context.Populate().GetEnumerator());
            
            LoadMenuBar();

            // load default page
            yield return UiService.ShowChild<DashboardViewModel>().In(this);
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            //AutomaticLoginTestPurpose();
            UiService.ShowDialog<LoginViewModel>()
                .Execute(new ActionExecutionContext { Target = this });
        }

        private static void AutomaticLoginTestPurpose()
        {
            var loginViewModel = IoC.Get<LoginViewModel>();
            loginViewModel.User = "costin.morariu@contoso.com";
            loginViewModel.Password = "Pwd";

            //var sequentialResult = new SequentialResult(loginViewModel.Login().GetEnumerator());
            //sequentialResult.Execute(new ActionExecutionContext());
            loginViewModel.Login().ExecuteSequential();
        }

        private void LoadMenuBar()
        {
            AddMenuItem<DashboardViewModel>(ShellStrings.DashboardMenuBarTitle, "Places-icon128.png");
            AddMenuItem<MyRequestsViewModel>(ShellStrings.MyRequestsMenuBarTitle, "Tasks-icon128.png");
            
            if (ApplicationContext.User.IsInRole(EmployeeRoles.Hr.ToString()))
                AddMenuItem<EmployeesViewModel>(ShellStrings.EmployeesMenuBarTitle, "Contacts-icon128.png");
        }

        private void AddMenuItem<TViewModel>(string title, string iconFileName)
            where TViewModel : IScreen
        {
            MenuBar.Menus.Add(new Menu(
                title, 
                () => Show<TViewModel>().ExecuteSequential(this), Configuration.IconsPath + iconFileName));
        }

        private IEnumerable<IResult> Show<TViewModel>()
            where TViewModel : IScreen
        {
            yield return UiService.ShowChild<TViewModel>();
        }
    }
}
using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.Dashboard;
using VacationManager.Ui.Components.MenuBar;
using VacationManager.Ui.Components.MyRequests;
using VacationManager.Ui.Components.Search;
using VacationManager.Ui.Infrastructure;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.Shell
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {
        #region External dependencies

        [Inject]
        public IMenuBarViewModel MenuBar { get; set; }

        [Inject]
        public ISearchViewModel Search { get; set; }

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

        protected override void OnInitialize()
        {
            base.OnInitialize();

            LoadMenuBar();

            // launch dashboard on startup
            Show<DashboardViewModel>().ExecuteSequential(this);
        }

        private void LoadMenuBar()
        {
            AddMenuItem<DashboardViewModel>(ShellStrings.DashboardMenuBarTitle, "Places-icon128.png");
            AddMenuItem<MyRequestsViewModel>(ShellStrings.MyRequestsMenuBarTitle, "Tasks-icon128.png");
            MenuBar.Menus.Add(new Menu(ShellStrings.EmployeesMenuBarTitle, () => { }, Configuration.IconsPath + "Contacts-icon128.png"));
        }

        private void AddMenuItem<TViewModel>(string title, string iconFileName)
            where TViewModel : IScreen
        {
            MenuBar.Menus.Add(new Menu(
                title, () => Show<TViewModel>().ExecuteSequential(this), Configuration.IconsPath + iconFileName));
        }

        private IEnumerable<IResult> Show<TViewModel>()
            where TViewModel : IScreen
        {
            yield return UiService.ShowChild<TViewModel>();
        }
    }
}
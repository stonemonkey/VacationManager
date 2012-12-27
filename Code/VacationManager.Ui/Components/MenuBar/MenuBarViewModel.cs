using Caliburn.Micro;
using VacationManager.Ui.Infrastructure;

namespace VacationManager.Ui.Components.MenuBar
{
    public class MenuBarViewModel : Screen, IMenuBarViewModel
    {
        public MenuCollection Menus { get; set; }

        public MenuBarViewModel()
        {
            Menus = new MenuCollection();
        }
    }
}
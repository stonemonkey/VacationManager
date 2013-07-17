using Caliburn.Micro;
using VacationManager.Ui.Components.MessageBox;
using VacationManager.Ui.Results;

namespace VacationManager.Ui.Services
{
    public interface IUiService
    {
        BusyResult ShowBusy(string message = null);

        BusyResult HideBusy();

        ActivateChildResult<TChild> ShowChild<TChild>() 
            where TChild : IScreen;

        ActivateChildResult<TChild> ShowChild<TChild>(TChild child)
            where TChild : IScreen;


        ActivateChildResult<TScreen> ShowDialog<TScreen>()
            where TScreen : IScreen;
        
        ActivateChildResult<TScreen> ShowDialog<TScreen>(TScreen screen) 
            where TScreen : IScreen;

        IResult ShowMessageBox(string message, string title = null, 
            MessageBoxOptions options = MessageBoxOptions.Ok, 
            System.Action<MessageBoxViewModel> callback = null);

        MessageBoxResult ShowWindowsMessageBox(string message, string caption, 
            System.Windows.MessageBoxButton button = System.Windows.MessageBoxButton.OK);
    }
}
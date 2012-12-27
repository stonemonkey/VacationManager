using Caliburn.Micro;
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

        MessageBoxResult ShowMessageBox(string message, string caption, 
            System.Windows.MessageBoxButton button = System.Windows.MessageBoxButton.OK);
    }
}
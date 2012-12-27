using Caliburn.Micro;
using VacationManager.Ui.Results;

namespace VacationManager.Ui.Services
{
    public class UiService : IUiService
    {
        public BusyResult ShowBusy(string message = null)
        {
            return BusyResult.Show(message);
        }

        public BusyResult HideBusy()
        {
            return BusyResult.Hide();
        }

        public MessageBoxResult ShowMessageBox(string message, string caption, 
            System.Windows.MessageBoxButton button = System.Windows.MessageBoxButton.OK)
        {
            return new MessageBoxResult(message, caption, button);
        }

        public ActivateChildResult<TChild> ShowChild<TChild>() 
            where TChild : IScreen
        {
            return new ActivateChildResult<TChild>();
        }

        public ActivateChildResult<TChild> ShowChild<TChild>(TChild child) 
            where TChild : IScreen
        {
            return new ActivateChildResult<TChild>(child);
        }
    }
}
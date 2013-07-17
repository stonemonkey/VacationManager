using Caliburn.Micro;
using VacationManager.Ui.Components.MessageBox;
using VacationManager.Ui.Components.Shell;
using VacationManager.Ui.Resources;
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

        public ActivateChildResult<TScreen> ShowDialog<TScreen>()
            where TScreen : IScreen
        {
            return ShowChild<TScreen>().In(IoC.Get<IShellViewModel>().DialogBox);
        }

        public ActivateChildResult<TScreen> ShowDialog<TScreen>(TScreen screen) 
            where TScreen : IScreen
        {
            return ShowChild(screen).In(IoC.Get<IShellViewModel>().DialogBox);
        }

        public IResult ShowMessageBox(string message, string title = null, 
            MessageBoxOptions options = MessageBoxOptions.Ok, System.Action<MessageBoxViewModel> callback = null)
        {
            var messageBoxViewModel = IoC.Get<MessageBoxViewModel>();

            messageBoxViewModel.DisplayName = title ?? GlobalStrings.ApplicationTitle;
            messageBoxViewModel.Options = options;
            messageBoxViewModel.Message = message;
            messageBoxViewModel.Callback = callback;

            return ShowDialog(messageBoxViewModel);
        }

        public MessageBoxResult ShowWindowsMessageBox(string message, string caption,
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
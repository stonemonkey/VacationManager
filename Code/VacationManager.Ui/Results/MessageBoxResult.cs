using System;
using System.Windows;
using Caliburn.Micro;

namespace VacationManager.Ui.Results
{
    public class MessageBoxResult : IResult
    {
        private readonly string _message;
        private readonly string _caption;
        private readonly MessageBoxButton _button;

        public event EventHandler<ResultCompletionEventArgs> Completed;

        public System.Windows.MessageBoxResult Result { get; private set; }

        public MessageBoxResult(string message, string caption, MessageBoxButton button)
        {
            _message = message;
            _caption = caption;
            _button = button;
        }

        public void Execute(ActionExecutionContext context = null)
        {
            Result = MessageBox.Show(_message, _caption, _button);

            if (Completed != null)
                Completed(this, new ResultCompletionEventArgs());
        }
    }
}
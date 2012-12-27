using System;
using System.Windows.Input;

namespace VacationManager.Ui.Infrastructure
{
    public class RelayCommand : ICommand
    {
        private readonly Action _handler;
        private readonly Func<bool> _isEnabled;

        public RelayCommand(Action handler) : this(handler, () => true)
        {
        }

        public RelayCommand(Action handler, Func<bool> isEnabled)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");    
            }

            if (isEnabled == null)
            {
                throw new ArgumentNullException("isEnabled");
            }

            _handler = handler;
            _isEnabled = isEnabled;
        }

        public bool CanExecute(object parameter)
        {
            return _isEnabled();
        }

        public void Execute(object parameter)
        {
            _handler();
        }

        public event EventHandler CanExecuteChanged;

        public void RiseCanExecuteChanged()
        {
            CanExecuteChanged(this, new EventArgs());
        }
    }
}


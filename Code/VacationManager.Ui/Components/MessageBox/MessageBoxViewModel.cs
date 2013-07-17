using System;
using Caliburn.Micro;

namespace VacationManager.Ui.Components.MessageBox
{
    public class MessageBoxViewModel : Screen
    {
        private MessageBoxOptions _selection;
        private Action<MessageBoxViewModel> _callback;

        #region Public Properties

        public bool OkVisible 
        {
            get { return IsVisible(MessageBoxOptions.Ok); }
        }

        public bool CancelVisible 
        {
            get { return IsVisible(MessageBoxOptions.Cancel); }
        }

        public bool YesVisible 
        {
            get { return IsVisible(MessageBoxOptions.Yes); }
        }

        public bool NoVisible 
        {
            get { return IsVisible(MessageBoxOptions.No); }
        }

        public string Message { get; set; }
        
        public MessageBoxOptions Options { get; set; }

        public Action<MessageBoxViewModel> Callback
        {
            get { return _callback; }
            set
            {
                _callback = value;

                Deactivated += MessageBoxDeactivated;
            }
        }

        #endregion

        public void Ok() 
        {
            Select(MessageBoxOptions.Ok);
        }

        public void Cancel() 
        {
            Select(MessageBoxOptions.Cancel);
        }

        public void Yes() 
        {
            Select(MessageBoxOptions.Yes);
        }

        public void No() 
        {
            Select(MessageBoxOptions.No);
        }

        public bool WasSelected(MessageBoxOptions option) 
        {
            return (_selection & option) == option;
        }

        #region Private methods

        private bool IsVisible(MessageBoxOptions option) 
        {
            return (Options & option) == option;
        }

        private void Select(MessageBoxOptions option) 
        {
            _selection = option;
            TryClose();
        }

        private void MessageBoxDeactivated(object sender, DeactivationEventArgs e)
        {
            if (Callback != null)
                Callback(this);

            Deactivated -= MessageBoxDeactivated;
        }

        #endregion
    }
}
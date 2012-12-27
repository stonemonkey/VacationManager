using System.Windows.Input;
using Caliburn.Micro;
using Action = System.Action;

namespace VacationManager.Ui.Infrastructure
{
    public class Menu : PropertyChangedBase
    {
        public string Name { get; set; }

        public ICommand Command { get; set; }

        public string ImagePath { get; set; }

        public Menu(string name, Action action, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
            Command = new RelayCommand(action);
        }
    }
}

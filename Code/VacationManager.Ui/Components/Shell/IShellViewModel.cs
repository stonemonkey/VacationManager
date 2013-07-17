using System.Collections.Generic;
using Caliburn.Micro;
using VacationManager.Ui.Components.DialogBox;

namespace VacationManager.Ui.Components.Shell
{
    public interface IShellViewModel : IConductor
    {
        IDialogBoxViewModel DialogBox { get; set; }
        
        IEnumerable<IResult> Load();
    }
}
using System.Collections.Generic;
using Caliburn.Micro;

namespace VacationManager.Ui.Components.LogIn
{
    public interface ILoginViewModel : IScreen
    {
        string User { get; set; }

        string Password { get; set; }

        IEnumerable<IResult> Login();
    }
}
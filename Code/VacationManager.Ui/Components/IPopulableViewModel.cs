using System.Collections.Generic;
using Caliburn.Micro;

namespace VacationManager.Ui.Components
{
    public interface IPopulableViewModel : IScreen
    {
        IEnumerable<IResult> Populate();
    }
}
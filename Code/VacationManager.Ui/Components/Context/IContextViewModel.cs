using VacationManager.Ui.BusinessObjects;

namespace VacationManager.Ui.Components.Context
{
    public interface IContextViewModel : IPopulableViewModel
    {
        Employee CurrentEmployee { get; }
    }
}
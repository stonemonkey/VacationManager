using BusinessObjects.Employees;

namespace VacationManager.Ui.Components.Context
{
    public interface IContextViewModel : IPopulableViewModel
    {
        Employee CurrentEmployee { get; }
    }
}
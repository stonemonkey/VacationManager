using Vm.BusinessObjects.Employees;

namespace VacationManager.Ui.Components.Context
{
    public interface IContextViewModel : IPopulableViewModel
    {
        Employee CurrentEmployee { get; }

        bool IsManager { get; }
        
        bool IsHr { get; }
    }
}
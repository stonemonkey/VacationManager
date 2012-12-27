using Caliburn.Micro;
using Csla;

namespace VacationManager.Ui.Components
{
    public interface IExplorerViewModel<TList, TObject> : IScreen
        where TList : ReadOnlyListBase<TList, TObject>
        where TObject : BusinessBase<TObject>
    {
        TList Items { get; set; }
    }
}
using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Csla;

namespace VacationManager.Ui.Results
{
    public class FetchListResult<TList, TObject> : ResultBase<TList>
        where TList : ReadOnlyListBase<TList, TObject>
        where TObject : BusinessBase<TObject>
    {
        private readonly object _criteria;

        public FetchListResult(object criteria = null)
        {
            _criteria = criteria;
        }

        public override async void Execute(ActionExecutionContext context = null)
        {
            try
            {
                // TODO: remove; for the moment symulates an time consuming operation
                await Task.Delay(500);

                if (_criteria == null)
                    Result = await DataPortal.FetchAsync<TList>();
                else
                    Result = await DataPortal.FetchAsync<TList>(_criteria);
            }
            catch (Exception e)
            {
                Error = e;
            }

            InvokeCompleted(new ResultCompletionEventArgs());
        }
    }
}
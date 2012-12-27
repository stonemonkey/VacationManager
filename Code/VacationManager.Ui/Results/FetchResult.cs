using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Csla;

namespace VacationManager.Ui.Results
{
    public class FetchResult<TObject> : ResultBase<TObject>
        where TObject : BusinessBase<TObject>
    {
        private readonly long _id;
        private readonly object _criteria;

        public FetchResult()
        {
        }
        
        public FetchResult(long id)
        {
            _id = id;
        }

        public FetchResult(object criteria)
        {
            _criteria = criteria;
        }

        public override async void Execute(ActionExecutionContext context = null)
        {
            try
            {
                // TODO: remove; for the moment symulates an time consuming operation
                await Task.Delay(500);

                if (_criteria != null)
                    Result = await DataPortal.FetchAsync<TObject>(_criteria);
                else if (_id != 0)
                    Result = await DataPortal.FetchAsync<TObject>(_id);
                else
                    Result = await DataPortal.FetchAsync<TObject>();
            }
            catch (Exception e)
            {
                Error = e;
            }

            InvokeCompleted(new ResultCompletionEventArgs());
        }
    }
}
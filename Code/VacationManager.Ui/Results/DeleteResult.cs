using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Csla;

namespace VacationManager.Ui.Results
{
    public class DeleteResult<TObject, TCriteria> : ResultBase<TObject>
        where TObject : BusinessBase<TObject>
    {
        private readonly TObject _obj;
        private readonly Func<TObject, TCriteria> _selector;

        public DeleteResult(TObject obj, Func<TObject, TCriteria> selector)
        {
            _obj = obj;
            _selector = selector;
        }

        public override async void Execute(ActionExecutionContext context = null)
        {
            try
            {
                // TODO: remove; for the moment symulates an time consuming operation
                await Task.Delay(500);

                await DataPortal.DeleteAsync<TObject>(_selector(_obj));
            }
            catch (Exception e)
            {
                Error = e;
            }

            InvokeCompleted(new ResultCompletionEventArgs());
        }
    }
}
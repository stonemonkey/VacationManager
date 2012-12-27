using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Csla;

namespace VacationManager.Ui.Results
{
    public class ExecuteResult<TObject> : ResultBase<TObject>
        where TObject : CommandBase<TObject>
    {
        private readonly TObject _obj;

        public ExecuteResult(TObject obj)
        {
            _obj = obj;
        }

        public override async void Execute(ActionExecutionContext context = null)
        {
            try
            {
                // TODO: remove; for the moment symulates an time consuming operation
                await Task.Delay(500);

                Result = await DataPortal.ExecuteAsync(_obj);
            }
            catch (Exception e)
            {
                Error = e;
            }

            InvokeCompleted(new ResultCompletionEventArgs());
        }
    }
}
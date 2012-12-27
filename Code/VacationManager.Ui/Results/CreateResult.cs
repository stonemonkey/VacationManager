using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Csla;

namespace VacationManager.Ui.Results
{
    public class CreateResult<TObject> : ResultBase<TObject>
        where TObject : BusinessBase<TObject>
    {
        public override async void Execute(ActionExecutionContext context = null)
        {
            try
            {
                // TODO: remove; for the moment symulates an time consuming operation
                await Task.Delay(500);

                Result = await DataPortal.CreateAsync<TObject>();
            }
            catch (Exception e)
            {
                Error = e;
            }

            InvokeCompleted(new ResultCompletionEventArgs());
        }
    }
}
using System;
using Caliburn.Micro;

namespace VacationManager.Ui.Results
{
    public abstract class ResultBase<TResult> : IResult
    {
        public TResult Result { get; set; }
        public Exception Error { get; set; }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        public abstract void Execute(ActionExecutionContext context = null);

        protected void InvokeCompleted(ResultCompletionEventArgs resultCompletionEventArgs)
        {
            if (Completed != null)
                Completed(this, resultCompletionEventArgs);
        }
    }
}
using System;
using Caliburn.Micro;
using VacationManager.Ui.Components;

namespace VacationManager.Ui.Results
{
    public class ActivateChildResult<TChild> : IResult
        where TChild : IScreen
    {
        private readonly Func<TChild> _locateChild;
        private Func<ActionExecutionContext, IConductor> _locateParent;

        private Action<TChild> _configureChildAction;
        
        public ActivateChildResult()
        {
            _locateChild = IoC.Get<TChild>;
        }

        public ActivateChildResult(TChild child)
        {
            _locateChild = () => child;
        }

        public ActivateChildResult<TChild> In(IConductor parent)
        {
            _locateParent = ctx => parent;
            return this;
        }
        
        public ActivateChildResult<TChild> In<TParent>()
        {
            _locateParent = ctx => (IConductor)IoC.Get<TParent>();
            return this;
        }

        public ActivateChildResult<TChild> Configure(Action<TChild> action)
        {
            _configureChildAction = action;
            return this;
        }

        public void Execute(ActionExecutionContext context)
        {
            var child = _locateChild();

            if (_configureChildAction != null)
                _configureChildAction(child);

            if (_locateParent == null)
                _locateParent = ctx => (IConductor) ctx.Target;

            var parent = _locateParent(context);

            parent.ActivateItem(child);

            var populable = child as IPopulableViewModel;
            if (populable != null)
                populable.Populate().ExecuteSequential(parent);

            if (Completed != null)
                Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}
using System.Collections.Generic;
using Caliburn.Micro;

namespace VacationManager.Ui.Components
{
    public static class EnumerableResultExtensions
    {
        public static void ExecuteSequential(this IEnumerable<IResult> result, object target = null)
        {
            var sr = new SequentialResult(result.GetEnumerator());
            sr.Execute(new ActionExecutionContext { Target = target });
        }
    }
}
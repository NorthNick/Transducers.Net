using System;

namespace Transducers.Net
{
    public class Reducer<TAccumulate, TSource> : IReducer<TAccumulate, TSource>
    {
        private readonly Func<TAccumulate, TSource, TAccumulate> _function;
        private readonly Func<ReductionStatus, TAccumulate, TSource, TAccumulate> _statusFunction;

        public ReductionStatus Status { get; private set; }

        public Reducer(ReductionStatus status, Func<TAccumulate, TSource, TAccumulate> function)
        {
            Status = status;
            _function = function;
        }

        public Reducer(ReductionStatus status, Func<ReductionStatus, TAccumulate, TSource, TAccumulate> statusFunction)
        {
            Status = status;
            _statusFunction = statusFunction;
        }

        public TAccumulate Apply(TAccumulate acc, TSource source)
        {
            return Status.Complete ? acc : _function == null ? _statusFunction(Status, acc, source) : _function(acc, source);
        }
    }
}

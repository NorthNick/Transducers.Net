using System;

namespace Transducers.Net
{
    class SelectTransducer<TIn, TOut> : ITransducer<TIn, TOut>
    {
        private readonly Func<TIn, TOut> _selector;

        public SelectTransducer(Func<TIn, TOut> selector)
        {
            _selector = selector;
        }

        public Func<TAcc, TIn, TAcc> Transform<TAcc>(ReductionStatus status, Func<TAcc, TOut, TAcc> reducer)
        {
            return (acc, source) => reducer(acc, _selector(source));
        }
    }
}

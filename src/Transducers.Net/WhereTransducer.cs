using System;

namespace Transducers.Net
{
    class WhereTransducer<TSource> : ITransducer<TSource, TSource>
    {
        private readonly Func<TSource, bool> _predicate;

        public WhereTransducer(Func<TSource, bool> predicate)
        {
            _predicate = predicate;
        }

        public Func<TAcc, TSource, TAcc> Apply<TAcc>(ReductionStatus status, Func<TAcc, TSource, TAcc> reducer)
        {
            return (acc, source) => _predicate(source) ? reducer(acc, source) : acc;
        }
    }
}

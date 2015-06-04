using System;

namespace Transducers.Net
{
    internal class SuffixTransducer<TSource> : ITransducer<TSource, TSource>
    {
        private readonly Func<Func<TSource, bool>> _prefixCompleteFactory;

        public SuffixTransducer(Func<Func<TSource, bool>> prefixCompleteFactory)
        {
            _prefixCompleteFactory = prefixCompleteFactory;
        }

        public Func<TAcc, TSource, TAcc> Transform<TAcc>(ReductionStatus status, Func<TAcc, TSource, TAcc> reducer)
        {
            var prefixComplete = _prefixCompleteFactory();
            bool skipping = true;
            return (acc, source) => {
                if (!skipping) {
                    return reducer(acc, source);
                } else if (prefixComplete(source)) {
                    skipping = false;
                    return reducer(acc, source);
                } else {
                    return acc;
                }
            };
        }
    }
}

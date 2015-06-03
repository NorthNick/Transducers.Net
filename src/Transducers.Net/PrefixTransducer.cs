﻿using System;

namespace Transducers.Net
{
    class PrefixTransducer<TSource> : ITransducer<TSource, TSource>
    {
        private readonly Func<Func<TSource, bool>> _prefixComplete;

        public PrefixTransducer(Func<Func<TSource, bool>> prefixComplete)
        {
            _prefixComplete = prefixComplete;
        }

        public Func<TAcc, TSource, TAcc> Apply<TAcc>(ReductionStatus status, Func<TAcc, TSource, TAcc> reducer)
        {
            var prefixComplete = _prefixComplete();
            return (acc, source) => {
                // TODO - decide whether to include this
                //if (status.Complete) return acc;
                if (prefixComplete(source)) {
                    status.Complete = true;
                    return acc;
                }
                return reducer(acc, source);
            };
        }
    }
}
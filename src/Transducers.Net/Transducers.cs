using System;
using System.Collections.Generic;

namespace Transducers.Net
{
    public static class Transducers
    {

        public static Func<IReducer<TAcc, TOut>, IReducer<TAcc, TIn>> Select<TAcc, TIn, TOut>(Func<TIn, TOut> selector)
        {
            return reducer => new Reducer<TAcc, TIn>(reducer.Status, (acc, source) => reducer.Apply(acc, selector(source)));
        }

        public static Func<IReducer<TAcc, TSource>, IReducer<TAcc, TSource>> Take<TAcc, TSource>(int n)
        {
            var counter = new Store<int>(n);
            return PrefixReducer<TAcc, TSource>(source => counter.PostApply(x => x - 1) <= 0);
        }

        public static Func<IReducer<TAcc, TSource>, IReducer<TAcc, TSource>> TakeWhile<TAcc, TSource>(Func<TSource, bool> predicate)
        {
            var taking = new Store<bool>(true);
            return PrefixReducer<TAcc, TSource>(source => !taking.PreApply(b => b && predicate(source)));
        }

        public static Func<IReducer<TAcc, TSource>, IReducer<TAcc, TSource>> Where<TAcc, TSource>(Func<TSource, bool> predicate)
        {
            return reducer => new Reducer<TAcc, TSource>(reducer.Status, (acc, source) => predicate(source) ? reducer.Apply(acc, source) : acc);
        }

        public static Func<TIn, TOut> Compose<TIn, TMid, TOut>(Func<TMid, TOut> fn1, Func<TIn, TMid> fn2)
        {
            return x => fn1(fn2(x));
        }

        public static TAcc Transduce<TAcc, TIn, TOut>(this IEnumerable<TIn> source,
            Func<IReducer<TAcc, TOut>, IReducer<TAcc, TIn>> transducer, Func<TAcc, TOut, TAcc> reducer, TAcc seed)
        {
            var sourceEnum = source.GetEnumerator();
            var status = new ReductionStatus();
            var res = seed;
            var red = transducer(new Reducer<TAcc, TOut>(status, reducer));
            while (!status.Complete && sourceEnum.MoveNext()) {
                res = red.Apply(res, sourceEnum.Current);
            }
            return res;
        }

        private static Func<IReducer<TAcc, TSource>, IReducer<TAcc, TSource>> PrefixReducer<TAcc, TSource>(Func<TSource, bool> prefixComplete)
        {
            return reducer => new Reducer<TAcc, TSource>(reducer.Status, (status, acc, source) => {
                if (status.Complete) return acc;
                if (prefixComplete(source)) {
                    status.Complete = true;
                    return acc;
                }
                return reducer.Apply(acc, source);
            });
        }

    }
}

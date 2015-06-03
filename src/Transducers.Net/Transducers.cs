using System;
using System.Collections.Generic;

namespace Transducers.Net
{
    public static class Transducers
    {
        public static ITransducer<TIn, TOut> Select<TIn, TOut>(Func<TIn, TOut> selector)
        {
            return new SelectTransducer<TIn, TOut>(selector);
        }

        public static ITransducer<TSource, TSource> Take<TSource>(int n)
        {
            return new PrefixTransducer<TSource>(() => {
                var counter = new Store<int>(n);
                return source => counter.PostApply(x => x - 1) <= 0;
            });
        }

        public static ITransducer<TSource, TSource> TakeWhile<TSource>(Func<TSource, bool> predicate)
        {
            return new PrefixTransducer<TSource>(() => {
                var taking = new Store<bool>(true);
                return source => !taking.PreApply(b => b && predicate(source));
            });
        }

        public static ITransducer<TSource, TSource> Where<TSource>(Func<TSource, bool> predicate)
        {
            return new WhereTransducer<TSource>(predicate);
        }

        public static ITransducer<TIn, TOut> Compose<TIn, TMid, TOut>(this ITransducer<TIn, TMid> transducer1, ITransducer<TMid, TOut> transducer2)
        {
            return new ComposeTransducer<TIn,TMid,TOut>(transducer1, transducer2);
        }

        public static TAcc Transduce<TAcc, TIn, TOut>(this IEnumerable<TIn> source, ITransducer<TIn, TOut> transducer, Func<TAcc, TOut, TAcc> reducer, TAcc seed)
        {
            var sourceEnum = source.GetEnumerator();
            var status = new ReductionStatus();
            var res = seed;
            var red = transducer.Apply(status, reducer);
            while (!status.Complete && sourceEnum.MoveNext()) {
                res = red(res, sourceEnum.Current);
            }
            return res;
        }

    }
}

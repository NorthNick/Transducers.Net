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

        public static ITransducer<TSource, TSource> Skip<TSource>(int n)
        {
            return new SuffixTransducer<TSource>(() => {
                var counter = new Store<int>(n);
                return source => counter.PostApply(x => x - 1) <= 0;
            });
        }

        public static ITransducer<TSource, TSource> SkipWhile<TSource>(Func<TSource, bool> predicate)
        {
            return new SuffixTransducer<TSource>(() => {
                var taking = new Store<bool>(true);
                return source => !taking.PreApply(b => b && predicate(source));
            });
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

        public static ITransducer<TIn, TOut> Then<TIn, TMid, TOut>(this ITransducer<TIn, TMid> first, ITransducer<TMid, TOut> next)
        {
            return new ComposeTransducer<TIn, TMid, TOut>(first, next);
        }

        public static ITransducer<TIn, TOut> Compose<TIn, TMid, TOut>(this ITransducer<TIn, TMid> first, ITransducer<TMid, TOut> next)
        {
            return new ComposeTransducer<TIn,TMid,TOut>(first, next);
        }

        public static TAcc Transduce<TAcc, TIn, TOut>(this IEnumerable<TIn> source, ITransducer<TIn, TOut> transducer, Func<TAcc, TOut, TAcc> reducer, TAcc seed)
        {
            var sourceEnum = source.GetEnumerator();
            var status = new ReductionStatus();
            var res = seed;
            var red = transducer.Transform(status, reducer);
            while (!status.Complete && sourceEnum.MoveNext()) {
                res = red(res, sourceEnum.Current);
            }
            return res;
        }

        public static IEnumerable<TOut> Transduce<TIn, TOut>(this IEnumerable<TIn> source, ITransducer<TIn, TOut> transducer)
        {
            var sourceEnum = source.GetEnumerator();
            var status = new ReductionStatus();
            var red = transducer.Transform<Optional<TOut>>(status, Utils.Enumerator);
            while (sourceEnum.MoveNext()) {
                var res = red(new Optional<TOut>(), sourceEnum.Current);
                if (status.Complete) break;
                if (res.HasValue) yield return res.Value;
            }
        }
    }
}

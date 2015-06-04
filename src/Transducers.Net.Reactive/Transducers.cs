using System;
using System.Reactive;

namespace Transducers.Net.Reactive
{
    public static class Transducers
    {
        public static IObservable<TAcc> Transduce<TAcc, TIn, TOut>(this IObservable<TIn> source, ITransducer<TIn, TOut> transducer, Func<TAcc, TOut, TAcc> reducer, TAcc seed)
        {
            var status = new ReductionStatus();
            var res = seed;
            var red = transducer.Transform(status, reducer);
            return new AnonymousObservable<TAcc>(observer => source.Subscribe(el => {
                if (status.Complete) {
                    observer.OnNext(res);
                    observer.OnCompleted();
                    return;
                }
                res = red(res, el);
            }, observer.OnError, () => {observer.OnNext(res); observer.OnCompleted();}));
        }

        public static IObservable<TOut> Transduce<TIn, TOut>(this IObservable<TIn> source, ITransducer<TIn, TOut> transducer)
        {
            var status = new ReductionStatus();
            var reducer = transducer.Transform<Optional<TOut>>(status, Utils.Enumerator);
            return new AnonymousObservable<TOut>(observer => source.Subscribe(el => {
                if (status.Complete) {
                    observer.OnCompleted();
                    return;
                }
                var res = reducer(new Optional<TOut>(), el);
                if (res.HasValue) {
                    observer.OnNext(res.Value);
                }
            }, observer.OnError, observer.OnCompleted));
        }

    }
}

using System;
using System.Reactive;

namespace Transducers.Net.Reactive
{
    public static class Transducers
    {
        public static IObservable<TOut> Transduce<TIn, TOut>(this IObservable<TIn> source, ITransducer<TIn, TOut> transducer)
        {
            var status = new ReductionStatus();
            var reducer = transducer.Transform<Optional<TOut>>(status, Utils.Enumerator);
            return new AnonymousObservable<TOut>(observer => source.Subscribe(el => {
                if (status.Complete) return;
                var res = reducer(new Optional<TOut>(), el);
                if (res.HasValue) {
                    observer.OnNext(res.Value);
                }
            }, observer.OnError, observer.OnCompleted));
        }
    }
}

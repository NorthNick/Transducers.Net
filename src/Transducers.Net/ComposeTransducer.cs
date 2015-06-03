using System;

namespace Transducers.Net
{
    class ComposeTransducer<TIn, TMid, TOut> : ITransducer<TIn, TOut>
    {
        private readonly ITransducer<TIn, TMid> _transducer1;
        private readonly ITransducer<TMid, TOut> _transducer2;

        public ComposeTransducer(ITransducer<TIn, TMid> transducer1, ITransducer<TMid, TOut> transducer2)
        {
            _transducer1 = transducer1;
            _transducer2 = transducer2;
        }

        public Func<TAcc, TIn, TAcc> Transform<TAcc>(ReductionStatus status, Func<TAcc, TOut, TAcc> reducer)
        {
            return _transducer1.Transform(status, _transducer2.Transform(status, reducer));
        }
    }
}

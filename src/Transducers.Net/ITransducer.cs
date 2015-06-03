using System;

namespace Transducers.Net
{
    public interface ITransducer<in TIn, out TOut>
    {
        Func<TAcc, TIn, TAcc> Transform<TAcc>(ReductionStatus status, Func<TAcc, TOut, TAcc> reducer);
    }
}

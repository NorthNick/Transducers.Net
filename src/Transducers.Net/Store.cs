using System;

namespace Transducers.Net
{
    class Store<TValue>
    {
        public TValue Value { get; private set; }

        public Store(TValue value)
        {
            Value = value;
        }

        public TValue PreApply(Func<TValue, TValue> fn)
        {
            Value = fn(Value);
            return Value;
        }

        public TValue PostApply(Func<TValue, TValue> fn)
        {
            var res = Value;
            Value = fn(Value);
            return res;
        }
    }
}

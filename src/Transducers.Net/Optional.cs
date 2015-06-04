namespace Transducers.Net
{
    public class Optional<T>
    {
        public bool HasValue { get; private set; }
        public T Value { get; private set; }

        public Optional() { } 

        public Optional(T value)
        {
            Value = value;
            HasValue = true;
        }
    }
}

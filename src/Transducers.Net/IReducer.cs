namespace Transducers.Net
{
    public interface IReducer<TAccumulate, in TSource>
    {
        ReductionStatus Status { get; }
        TAccumulate Apply(TAccumulate acc, TSource source);
    }
}
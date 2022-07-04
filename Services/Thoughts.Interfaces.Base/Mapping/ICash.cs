namespace Thoughts.Interfaces.Base.Mapping
{
    public interface ICash<TKey, TValue>
    {
        Dictionary<TKey, TValue> Cash { get; }
    }
}

namespace Thoughts.Interfaces.Base.Mapping
{
    public interface ICash<TKey, TValue> : IDisposable
    {
        Dictionary<TKey, TValue> Cash { get; }
    }
}

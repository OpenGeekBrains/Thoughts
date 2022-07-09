using TagDom = Thoughts.Domain.Base.Entities.Tag;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class TagDomainCash : ICash<int, TagDom>
    {
        public Dictionary<int, TagDom> Cash { get; private set; } = new();

        bool disposed = false;
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                Cash = null;
            }
            disposed = true;
        }
    }
}

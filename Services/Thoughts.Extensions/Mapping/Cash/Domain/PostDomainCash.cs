using PostDom = Thoughts.Domain.Base.Entities.Post;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class PostDomainCash : ICash<int, PostDom>
    {
        public Dictionary<int, PostDom> Cash { get; private set; } = new();

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

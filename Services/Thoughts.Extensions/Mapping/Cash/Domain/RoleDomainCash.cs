using RoleDom = Thoughts.Domain.Base.Entities.Role;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class RoleDomainCash : ICash<int, RoleDom>
    {
        public Dictionary<int, RoleDom> Cash { get; private set; } = new();

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

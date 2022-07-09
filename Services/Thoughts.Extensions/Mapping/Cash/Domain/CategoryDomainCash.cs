using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class CategoryDomainCash : ICash<int, CategoryDom>
    {
        public Dictionary<int, CategoryDom> Cash { get; private set; } = new();

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

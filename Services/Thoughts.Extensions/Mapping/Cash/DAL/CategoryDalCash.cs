using CategoryDal = Thoughts.DAL.Entities.Category;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.DAL
{
    public class CategoryDalCash : ICash<int, CategoryDal>
    {
        public Dictionary<int, CategoryDal> Cash { get; private set; } = new();

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

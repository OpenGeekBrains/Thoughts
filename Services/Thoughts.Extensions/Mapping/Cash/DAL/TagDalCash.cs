using TagDal = Thoughts.DAL.Entities.Tag;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.DAL
{
    public class TagDalCash : ICash<int, TagDal>
    {
        public Dictionary<int, TagDal> Cash { get; private set; } = new();

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

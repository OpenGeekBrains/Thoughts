using RoleDal = Thoughts.DAL.Entities.Role;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.DAL
{
    public class RoleDalCash : ICash<int, RoleDal>
    {
        public Dictionary<int, RoleDal> Cash { get; private set; } = new();

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

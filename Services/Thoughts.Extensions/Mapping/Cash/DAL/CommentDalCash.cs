using CommentDal = Thoughts.DAL.Entities.Comment;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.DAL
{
    public class CommentDalCash : ICash<int, CommentDal>
    {
        public Dictionary<int, CommentDal> Cash { get; private set; } = new();

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

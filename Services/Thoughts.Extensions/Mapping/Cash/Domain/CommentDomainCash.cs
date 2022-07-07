using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class CommentDomainCash : ICash<int, CommentDom>
    {
        public Dictionary<int, CommentDom> Cash { get; private set; } = new();

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

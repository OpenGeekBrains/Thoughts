using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class CommentDomainCash : ICash<int, CommentDom>
    {
        public Dictionary<int, CommentDom> Cash { get; } = new();
    }
}

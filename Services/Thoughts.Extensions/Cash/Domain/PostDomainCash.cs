using PostDom = Thoughts.Domain.Base.Entities.Post;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Cash.Domain
{
    public class PostDomainCash : ICash<int, PostDom>
    {
        public Dictionary<int, PostDom> Cash { get; } = new();
    }
}

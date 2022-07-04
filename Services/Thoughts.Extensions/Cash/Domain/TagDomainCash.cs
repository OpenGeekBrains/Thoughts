using TagDom = Thoughts.Domain.Base.Entities.Tag;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Cash.Domain
{
    public class TagDomainCash : ICash<int, TagDom>
    {
        public Dictionary<int, TagDom> Cash { get; } = new();
    }
}

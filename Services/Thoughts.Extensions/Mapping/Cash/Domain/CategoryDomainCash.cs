using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class CategoryDomainCash : ICash<int, CategoryDom>
    {
        public Dictionary<int, CategoryDom> Cash { get; } = new();
    }
}

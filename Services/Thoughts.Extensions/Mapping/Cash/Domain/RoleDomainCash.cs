using RoleDom = Thoughts.Domain.Base.Entities.Role;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class RoleDomainCash : ICash<int, RoleDom>
    {
        public Dictionary<int, RoleDom> Cash { get; } = new();
    }
}

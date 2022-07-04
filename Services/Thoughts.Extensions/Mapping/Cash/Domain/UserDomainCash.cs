using UserDom = Thoughts.Domain.Base.Entities.User;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.Domain
{
    public class UserDomainCash : ICash<string, UserDom>
    {
        public Dictionary<string, UserDom> Cash { get; } = new();
    }
}

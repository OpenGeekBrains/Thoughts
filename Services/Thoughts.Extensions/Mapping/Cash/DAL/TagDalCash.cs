using TagDal = Thoughts.DAL.Entities.Tag;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.DAL
{
    public class TagDalCash : ICash<int, TagDal>
    {
        public Dictionary<int, TagDal> Cash { get; } = new();
    }
}

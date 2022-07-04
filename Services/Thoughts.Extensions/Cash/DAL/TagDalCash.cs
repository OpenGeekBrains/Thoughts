using TagDal = Thoughts.DAL.Entities.Tag;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Cash.DAL
{
    public class TagDalCash : ICash<int, TagDal>
    {
        public Dictionary<int, TagDal> Cash { get; } = new();
    }
}

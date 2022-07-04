using CategoryDal = Thoughts.DAL.Entities.Category;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Cash.DAL
{
    public class CategoryDalCash : ICash<int, CategoryDal>
    {
        public Dictionary<int, CategoryDal> Cash { get; } = new();
    }
}

using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;

using Xunit;

using CategoryDom = Thoughts.Domain.Base.Entities.Category;
namespace Thoughts.Mapping.Tests.Maps
{
    public class CategoryMapperTests
    {
        private readonly IMapper<CategoryDom, Category> _mapper;

        public CategoryMapperTests(IMapper<CategoryDom, Category> mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public void MapperIsNotNull()
        {
            Assert.True(_mapper is not null);
        }
    }
}

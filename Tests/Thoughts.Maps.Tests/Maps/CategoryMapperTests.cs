using System.Linq;
using Thoughts.Extensions.Maps;
using Xunit;
using Thoughts.Services.Data;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CategoryDAL = Thoughts.DAL.Entities.Category;

namespace Thoughts.Maps.Tests.Maps
{
    public class CategoryMapperTests
    {
        CategoryMapper mapper;
        CategoryDAL categoryForTest;

        public CategoryMapperTests()
        {
            mapper = new CategoryMapper();
            categoryForTest = TestDbData.Categories.First();
        }

        [Fact]
        public void MapAndBackTest()
        {
            var testDomRes = mapper.Map(categoryForTest);

            Assert.NotNull(testDomRes);
            Assert.True(testDomRes is CategoryDom);
            Assert.Equal(testDomRes.Id, categoryForTest.Id);
            Assert.Equal(testDomRes.Name, categoryForTest.Name);

            var testDalRes = mapper.Map(testDomRes);

            Assert.NotNull(testDalRes);
            Assert.True(testDalRes is CategoryDAL);
            Assert.Equal(testDalRes.Id, categoryForTest.Id);
            Assert.Equal(testDalRes.Name, categoryForTest.Name);
        }

        [Fact]
        public void MapNull_ReturnsNull()
        {
            CategoryDom categoryDom = null;
            var testDalRes = mapper.Map(categoryDom);

            Assert.Null(testDalRes);
        }
    }
}

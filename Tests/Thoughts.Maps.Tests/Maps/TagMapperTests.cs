using TagDAL = Thoughts.DAL.Entities.Tag;
using TagDOM = Thoughts.Domain.Base.Entities.Tag;
using Xunit;
using Thoughts.Extensions.Maps;

namespace Thoughts.Maps.Tests.Maps
{
    public class TagMapperTests
    {
        TagMapper mapper;
        TagDAL tagForTest;

        public TagMapperTests()
        {
            mapper = new TagMapper();
            tagForTest = new TagDAL()
            {
                Id = 1,
                Name = "1",
            };
        }

        [Fact]
        public void MapAndBackTest()
        {
            var testDomRes = mapper.Map(tagForTest);

            Assert.NotNull(testDomRes);
            Assert.True(testDomRes is TagDOM);
            Assert.Equal(testDomRes.Id, tagForTest.Id);
            Assert.Equal(testDomRes.Name, tagForTest.Name);

            var testDalRes = mapper.Map(testDomRes);

            Assert.NotNull(testDalRes);
            Assert.True(testDalRes is TagDAL);
            Assert.Equal(testDalRes.Id, tagForTest.Id);
            Assert.Equal(testDalRes.Name, tagForTest.Name);
        }

        [Theory]
        [InlineData(null)]
        public void MapNull_ReturnsNull(TagDOM item)
        {
            var testDalRes = mapper.Map(item);

            Assert.Null(testDalRes);
        }
    }
}

using PostDAL = Thoughts.DAL.Entities.Post;
using PostDOM = Thoughts.Domain.Base.Entities.Post;
using Xunit;
using Thoughts.Extensions.Maps;
using Thoughts.Services.Data;
using System.Linq;

namespace Thoughts.Maps.Tests.Maps
{
    public class PostMapperTests
    {
        PostMapper mapper;
        PostDAL postForTest;
        public PostMapperTests()
        {
            mapper = new PostMapper();
            postForTest = TestDbData.Posts.First();
        }

        [Fact]
        public void MapAndBackTest()
        {
            var testDomRes = mapper.Map(postForTest);

            Assert.NotNull(testDomRes);
            Assert.True(testDomRes is PostDOM);
            Assert.Equal(testDomRes.Id, postForTest.Id);
            Assert.Equal(testDomRes.Date, postForTest.Date);
            Assert.Equal(testDomRes.Title, postForTest.Title);
            Assert.Equal(testDomRes.Body, postForTest.Body);

            var testDalRes = mapper.Map(testDomRes);

            Assert.NotNull(testDalRes);
            Assert.True(testDalRes is PostDAL);
            Assert.Equal(testDalRes.Id, postForTest.Id);
            Assert.Equal(testDalRes.Date, postForTest.Date);
            Assert.Equal(testDalRes.Title, postForTest.Title);
            Assert.Equal(testDalRes.Body, postForTest.Body);
        }

        [Theory]
        [InlineData(null)]
        public void MapNull_ReturnsNull(PostDOM item)
        {
            var testDalRes = mapper.Map(item);

            Assert.Null(testDalRes);
        }
    }
}

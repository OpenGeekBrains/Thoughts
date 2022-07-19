using CommentDAL = Thoughts.DAL.Entities.Comment;
using CommentDOM = Thoughts.Domain.Base.Entities.Comment;
using Xunit;
using Thoughts.Extensions.Maps;
using Thoughts.Services.Data;
using System.Linq;

namespace Thoughts.Maps.Tests.Maps
{
    public class CommentMapperTests
    {
        CommentMapper mapper;
        CommentDAL commentForTest;
        public CommentMapperTests()
        {
            mapper = new CommentMapper();
            commentForTest = new CommentDAL()
            {
                Id = 1,
                Body = "Comment For Test",
                IsDeleted = false,
                Date = System.DateTimeOffset.Now,
                ParentComment = new CommentDAL()
                {
                    Id = 2,
                    Body = "Parent Comment",
                    User = TestDbData.Users.First(),
                },
                ChildrenComment = null,
                User = TestDbData.Users.First(),
            };
        }

        [Fact]
        public void MapAndBackTest()
        {
            var testDomRes = mapper.Map(commentForTest);

            Assert.NotNull(testDomRes);
            Assert.True(testDomRes is CommentDOM);
            Assert.Equal(testDomRes.Id, commentForTest.Id);
            Assert.Equal(testDomRes.Body, commentForTest.Body);
            Assert.Equal(testDomRes.IsDeleted, commentForTest.IsDeleted);
            Assert.Equal(testDomRes.Date, commentForTest.Date);

            var testDalRes = mapper.Map(testDomRes);

            Assert.NotNull(testDalRes);
            Assert.True(testDalRes is CommentDAL);
            Assert.Equal(testDalRes.Id, commentForTest.Id);
            Assert.Equal(testDalRes.Body, commentForTest.Body);
            Assert.Equal(testDalRes.IsDeleted, commentForTest.IsDeleted);
            Assert.Equal(testDalRes.Date, commentForTest.Date);
        }

        [Theory]
        [InlineData(null)]
        public void MapNull_ReturnsNull(CommentDOM item)
        {
            var testDalRes = mapper.Map(item);

            Assert.Null(testDalRes);
        }
    }
}

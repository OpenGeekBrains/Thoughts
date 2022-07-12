using UserDom = Thoughts.Domain.Base.Entities.User;
using UserDAl = Thoughts.DAL.Entities.User;
using Xunit;
using Thoughts.Extensions.Maps;
using Thoughts.Services.Data;
using System.Linq;

namespace Thoughts.Maps.Tests.Maps
{
    public class UserMapperTests
    {
        UserMapper mapper;
        UserDAl userForTest;

        public UserMapperTests()
        {
            mapper = new UserMapper();
            userForTest = TestDbData.Users.First();
        }

        [Fact]
        public void MapAndBackTest()
        {
            var testDomRes = mapper.Map(userForTest);

            Assert.NotNull(testDomRes);
            Assert.True(testDomRes is UserDom);
            Assert.Equal(testDomRes.Id, userForTest.Id);
            Assert.Equal(testDomRes.NickName, userForTest.NickName);
            Assert.Equal(testDomRes.LastName, userForTest.LastName);
            Assert.Equal(testDomRes.FirstName, userForTest.FirstName);
            Assert.Equal(testDomRes.Patronymic, userForTest.Patronymic);
            Assert.Equal(testDomRes.Birthday, userForTest.Birthday);

            var testDalRes = mapper.Map(testDomRes);

            Assert.NotNull(testDalRes);
            Assert.True(testDalRes is UserDAl);
            Assert.Equal(testDalRes.Id, userForTest.Id);
            Assert.Equal(testDalRes.NickName, userForTest.NickName);
            Assert.Equal(testDalRes.LastName, userForTest.LastName);
            Assert.Equal(testDalRes.FirstName, userForTest.FirstName);
            Assert.Equal(testDalRes.Patronymic, userForTest.Patronymic);
            Assert.Equal(testDalRes.Birthday, userForTest.Birthday);
        }

        [Theory]
        [InlineData(null)]
        public void MapNull_ReturnsNull(UserDom item)
        {
            var testDalRes = mapper.Map(item);

            Assert.Null(testDalRes);
        }
    }
}

using RoleDAL = Thoughts.DAL.Entities.Role;
using RoleDOM = Thoughts.Domain.Base.Entities.Role;
using Xunit;
using Thoughts.Extensions.Maps;
using Thoughts.Services.Data;

namespace Thoughts.Maps.Tests.Maps
{
    public class RoleMapperTests
    {
        RoleMapper mapper;
        RoleDAL roleForTest;

        public RoleMapperTests()
        {
            mapper = new RoleMapper();
            roleForTest = new RoleDAL
            {
                Id = 1,
                Name = "1",
                Users = TestDbData.Users
            };
        }

        [Fact]
        public void MapAndBackTest()
        {
            var testDomRes = mapper.Map(roleForTest);

            Assert.NotNull(testDomRes);
            Assert.True(testDomRes is RoleDOM);
            Assert.Equal(testDomRes.Id, roleForTest.Id);
            Assert.Equal(testDomRes.Name, roleForTest.Name);

            var testDalRes = mapper.Map(testDomRes);

            Assert.NotNull(testDalRes);
            Assert.True(testDalRes is RoleDAL);
            Assert.Equal(testDalRes.Id, roleForTest.Id);
            Assert.Equal(testDalRes.Name, roleForTest.Name);
        }

        [Theory]
        [InlineData(null)]
        public void MapNull_ReturnsNull(RoleDOM item)
        {
            var testDalRes = mapper.Map(item);

            Assert.Null(testDalRes);
        }

    }
}

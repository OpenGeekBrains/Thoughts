using Thoughts.Maps.Tests.Fixtures;
using Thoughts.Services.Mapping;
using UserDom = Thoughts.Domain.Base.Entities.User;
using UserDAL = Thoughts.DAL.Entities.User;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Thoughts.Services.InSQL;
using Thoughts.Extensions.Maps;
using System;
using System.Linq;
using Thoughts.Services.Data;

namespace Thoughts.Maps.Tests.Mapping
{
    [Collection("Database collection")]
    public class UserMappingRepositoryTests
    {
        DbFixture _fixture;
        MappingRepository<UserDAL, UserDom, string> _repo;
        DbRepository<UserDAL, string> _dbRepository;
        Mock<ILogger<DbRepository<UserDAL, string>>> _dbRepoMock;
        UserMapper _mapper;

        public UserMappingRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _mapper = new UserMapper();
            _dbRepoMock = new Mock<ILogger<DbRepository<UserDAL, string>>>();
            _dbRepository = new DbRepository<UserDAL, string>(_fixture.DB, _dbRepoMock.Object);
            _repo = new MappingRepository<UserDAL, UserDom, string>(_dbRepository, _mapper);
        }

        [Fact]
        public void RepoCreated()
        {
            Assert.NotNull(_repo);
        }

        [Fact]
        public async void GetAllTest()
        {
            var result = await _repo.GetAll();
            Assert.NotNull(result);
            foreach (var item in result)
            {
                Assert.NotNull(item);
                Assert.True(item is UserDom);
            }
        }

        [Fact]
        public async void ExistIdTest_ReturnTrue()
        {
            var id = TestDbData.Users.First().Id;
            var result = await _repo.ExistId(id);
            Assert.True(result);
        }

        [Theory]
        [InlineData("404")]
        public async void ExistIdTest_ReturnFalse(string id)
        {
            var result = await _repo.ExistId(id);
            Assert.False(result);
        }

        [Fact]
        public async void ExistTest_ReturnTrue()
        {
            var item = _mapper.Map(TestDbData.Users.First());
            var result = await _repo.Exist(item!);

            Assert.True(result);
        }

        [Theory]
        [InlineData("404", "User404")]
        public async void ExistTest_ReturnFalse(string id, string nickName)
        {
            var item = new UserDom { Id = id, NickName = nickName };

            var result = await _repo.Exist(item);

            Assert.False(result);
        }

        [Fact]
        public async void ExistTest_ThrownException_WithNullAgrument()
        {
            bool catched = false;
            try
            {
                var result = await _repo.Exist(null);
            }
            catch (ArgumentNullException e)
            {
                catched = true;
                Assert.True(e is ArgumentNullException);
            }
            Assert.True(catched);
        }

        [Fact]
        public async void GetCountTest()
        {
            var result = await _repo.GetCount();

            Assert.NotEqual(0, result);
            Assert.True(result >= 4);
        }

        [Fact]
        public async void GetByIdTest()
        {
            var id = TestDbData.Users.First().Id;
            var result = await _repo.GetById(id);
            Assert.True(result is UserDom);
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData("404")]
        public async void GetById_ReturnNull(string id)
        {
            var result = await _repo.GetById(id);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("User4")]
        public async void AddTest(string name)
        {
            var userDom = new UserDom()
            {
                FirstName = name,
                LastName = name,
                NickName = name
            };

            var result = await _repo.Add(userDom);

            Assert.NotNull(result);
            Assert.NotEqual("0", result.Id);
            Assert.Equal(userDom.NickName, result.NickName);
        }

        [Fact]
        public async void AddRangeTest()
        {
            string[] names = new string[3] { "NewUser1", "NewUser2", "NewUser3" };
            var usersDom = new UserDom[3];
            for (int i = 0; i < usersDom.Length; i++)
            {
                usersDom[i] = new UserDom
                {
                    FirstName = names[i],
                    LastName = names[i],
                    NickName = names[i],
                };
            }

            await _repo.AddRange(usersDom);

            Assert.NotNull(_fixture.DB.Users.FirstOrDefault(i => i.NickName == names[0]));
            Assert.NotNull(_fixture.DB.Users.FirstOrDefault(i => i.NickName == names[1]));
            Assert.NotNull(_fixture.DB.Users.FirstOrDefault(i => i.NickName == names[2]));
        }

        // todo:
        // GetPageTest()
        // UpdateTest()
        // UpdateRangeTest()
        // DeleteTest()
        // DeleteRangeTest()
        // DeleteByIdTest()
    }
}

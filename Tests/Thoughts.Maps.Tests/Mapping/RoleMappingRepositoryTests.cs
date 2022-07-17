using Thoughts.Maps.Tests.Fixtures;
using Thoughts.Services.Mapping;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using RoleDAL = Thoughts.DAL.Entities.Role;
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
    public class RoleMappingRepositoryTests
    {
        DbFixture _fixture;
        MappingRepository<RoleDAL, RoleDom> _repo;
        DbRepository<RoleDAL> _dbRepository;
        Mock<ILogger<DbRepository<RoleDAL>>> _dbRepoMock;
        RoleMapper _mapper;

        public RoleMappingRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _mapper = new RoleMapper();
            _dbRepoMock = new Mock<ILogger<DbRepository<RoleDAL>>>();
            _dbRepository = new DbRepository<RoleDAL>(_fixture.DB, _dbRepoMock.Object);
            _repo = new MappingRepository<RoleDAL, RoleDom>(_dbRepository, _mapper);
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
                Assert.True(item is RoleDom);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void ExistIdTest_ReturnTrue(int id)
        {
            var result = await _repo.ExistId(id);
            Assert.True(result);
        }

        [Theory]
        [InlineData(404)]
        public async void ExistIdTest_ReturnFalse(int id)
        {
            var result = await _repo.ExistId(id);
            Assert.False(result);
        }

        [Fact]
        public async void ExistTest_ReturnTrue()
        {
            var item = _mapper.Map(TestDbData.Roles.First());
            var result = await _repo.Exist(item);

            Assert.True(result);
        }

        [Theory]
        [InlineData(404, "Role404")]
        public async void ExistTest_ReturnFalse(int id, string name)
        {
            var item = new RoleDom { Id = id, Name = name };

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
            Assert.True(result >= 2);
        }

        [Theory]
        [InlineData(1, 1)]
        public async void GetTest(int skip, int count)
        {
            var result = await _repo.Get(skip, count);

            bool correct = true;

            if (result.FirstOrDefault(s => s.Id == 2) is not null)
            {
                Assert.True(result.FirstOrDefault(s => s.Id == 2) is RoleDom);
            }
            else correct = false;

            Assert.True(correct);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetByIdTest(int id)
        {
            var result = await _repo.GetById(id);
            Assert.True(result is RoleDom);
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData(404)]
        public async void GetById_ReturnNull(int id)
        {
            var result = await _repo.GetById(id);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Role3")]
        public async void AddTest(string name)
        {
            var roleDom = new RoleDom()
            {
                Name = name,
            };

            var result = await _repo.Add(roleDom);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(roleDom.Name, result.Name);
        }

        [Fact]
        public async void AddRangeTest()
        {
            string[] names = new string[3] { "NewRole1", "NewRole2", "NewRole3" };
            var rolesDom = new RoleDom[3];
            for (int i = 0; i < rolesDom.Length; i++)
            {
                rolesDom[i] = new RoleDom
                {
                    Name = names[i],
                };
            }

            await _repo.AddRange(rolesDom);

            Assert.NotNull(_fixture.DB.Roles.FirstOrDefault(i => i.Name == names[0]));
            Assert.NotNull(_fixture.DB.Roles.FirstOrDefault(i => i.Name == names[1]));
            Assert.NotNull(_fixture.DB.Roles.FirstOrDefault(i => i.Name == names[2]));
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

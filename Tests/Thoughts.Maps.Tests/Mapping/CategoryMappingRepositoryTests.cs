
using Thoughts.Maps.Tests.Fixtures;
using Thoughts.Services.Mapping;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CategoryDAL = Thoughts.DAL.Entities.Category;

using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Thoughts.Services.InSQL;
using Thoughts.Extensions.Maps;

namespace Thoughts.Maps.Tests.Mapping
{
    [Collection("Database collection")]
    public class CategoryMappingRepositoryTests
    {
        DbFixture _fixture;
        MappingRepository<CategoryDAL, CategoryDom> _repo;
        DbRepository<CategoryDAL> _dbRepository;
        Mock<ILogger<DbRepository<CategoryDAL>>> _dbRepoMock;
        CategoryMapper _mapper;
        public CategoryMappingRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _mapper = new CategoryMapper();
            _dbRepoMock = new Mock<ILogger<DbRepository<CategoryDAL>>>();
            _dbRepository = new DbRepository<CategoryDAL>(_fixture.DB, _dbRepoMock.Object);
            _repo = new MappingRepository<CategoryDAL, CategoryDom>(_dbRepository, _mapper);
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
                Assert.True(item is CategoryDom);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void ExistIdTest_ReturnTrue(int id)
        {
            var result = await _repo.ExistId(id);
            Assert.True(result);
        }

        [Theory]
        [InlineData(4)]
        public async void ExistIdTest_ReturnFalse(int id)
        {
            var result = await _repo.ExistId(id);
            Assert.False(result);
        }
    }
}

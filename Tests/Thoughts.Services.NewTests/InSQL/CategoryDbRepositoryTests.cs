using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Xunit;
using Moq;
using Thoughts.DAL.Entities;
using Thoughts.Services.InSQL;
using Thoughts.Services.NewTests.Fixtures;


namespace Thoughts.Services.NewTests.InSQL
{
    [Collection("Database collection")]
    public class CategoryDbRepositoryTests
    {
        DbFixture _fixture;
        Mock<ILogger<DbRepository<Category>>> _dbRepoLoggerMock;
        DbRepository<Category> _repo;
        public CategoryDbRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _dbRepoLoggerMock = new Mock<ILogger<DbRepository<Category>>>();
            _repo = new DbRepository<Category>(_fixture.DB, _dbRepoLoggerMock.Object);
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
                Assert.True(item is Category);
            }
        }
    }
}

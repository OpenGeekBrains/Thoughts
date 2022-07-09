using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Moq;

using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Base.Repositories;
using Thoughts.Mapping.Tests.Maps.Fixtures;
using Thoughts.Services.InSQL;
using Thoughts.Services.Mapping;

using Xunit;

using CategoryDom = Thoughts.Domain.Base.Entities.Category;

namespace Thoughts.Mapping.Tests.Repositories
{
    public class CategoryMappingRepositoryTests : IClassFixture<DataBaseFixture>
    {
        private readonly MappingRepository<Category, CategoryDom> _mapperRepo;
        private Mock<ILogger<DbRepository<Category>>> _loggerCategoryMock;
        public CategoryMappingRepositoryTests(DataBaseFixture fixture, IMapper<Category, CategoryDom> mapper)
        {
            _loggerCategoryMock = new Mock<ILogger<DbRepository<Category>>>();
            var dbRepository = new DbRepository<Category>(fixture.DB, _loggerCategoryMock.Object);

            //_mapperRepo = new MappingRepository<Category, CategoryDom>(dbRepository, mapper);
        }
        [Fact]
        public void RepoCreated()
        {
            Assert.True(_mapperRepo is not null);
        }
    }
}

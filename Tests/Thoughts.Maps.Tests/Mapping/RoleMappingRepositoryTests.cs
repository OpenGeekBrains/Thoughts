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
    }
}

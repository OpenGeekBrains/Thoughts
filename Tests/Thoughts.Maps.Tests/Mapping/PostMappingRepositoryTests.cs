using Thoughts.Maps.Tests.Fixtures;
using Thoughts.Services.Mapping;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using PostDAL = Thoughts.DAL.Entities.Post;
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
    public class PostMappingRepositoryTests
    {
        DbFixture _fixture;
        MappingRepository<PostDAL, PostDom> _repo;
        DbRepository<PostDAL> _dbRepository;
        Mock<ILogger<DbRepository<PostDAL>>> _dbRepoMock;
        PostMapper _mapper;

        public PostMappingRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _mapper = new PostMapper();
            _dbRepoMock = new Mock<ILogger<DbRepository<PostDAL>>>();
            _dbRepository = new DbRepository<PostDAL>(_fixture.DB, _dbRepoMock.Object);
            _repo = new MappingRepository<PostDAL, PostDom>(_dbRepository, _mapper);
        }
    }
}

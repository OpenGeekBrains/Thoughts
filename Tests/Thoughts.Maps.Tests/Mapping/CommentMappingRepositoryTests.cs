using Thoughts.Maps.Tests.Fixtures;
using Thoughts.Services.Mapping;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using CommentDAL = Thoughts.DAL.Entities.Comment;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Thoughts.Services.InSQL;
using Thoughts.Extensions.Maps;
using System;
using System.Linq;

namespace Thoughts.Maps.Tests.Mapping
{
    [Collection("Database collection")]
    public class CommentMappingRepositoryTests
    {
        DbFixture _fixture;
        MappingRepository<CommentDAL, CommentDom> _repo;
        DbRepository<CommentDAL> _dbRepository;
        Mock<ILogger<DbRepository<CommentDAL>>> _dbRepoMock;
        CommentMapper _mapper;

        public CommentMappingRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _mapper = new CommentMapper();
            _dbRepoMock = new Mock<ILogger<DbRepository<CommentDAL>>>();
            _dbRepository = new DbRepository<CommentDAL>(_fixture.DB, _dbRepoMock.Object);
            _repo = new MappingRepository<CommentDAL, CommentDom>(_dbRepository, _mapper);
        }
    }
}

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
using Thoughts.Services.Data;

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
                Assert.True(item is CommentDom);
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
        [InlineData(404)]
        public async void ExistIdTest_ReturnFalse(int id)
        {
            var result = await _repo.ExistId(id);
            Assert.False(result);
        }

        [Fact]
        public async void ExistTest_ReturnTrue()
        {
            var item = _mapper.Map(TestDbData.Comments.First());
            var result = await _repo.Exist(item);

            Assert.True(result);
        }

        [Theory]
        [InlineData(404, "Comment404")]
        public async void ExistTest_ReturnFalse(int id, string body)
        {
            var item = _mapper.Map(TestDbData.Comments.First());
            item.Id = id;
            item.Body = body;

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
            Assert.True(result >= 3);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public async void GetTest(int skip, int count)
        {
            var result = await _repo.Get(skip, count);

            bool correct = true;

            switch (result.Count())
            {
                case 1:
                    if (result.FirstOrDefault(s => s.Id == 3) is not null)
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is CommentDom);
                    else if (result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is CommentDom);
                    }
                    else correct = false;
                    break;
                case 2:
                    if (result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is CommentDom);
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is CommentDom);
                    }
                    else correct = false;
                    break;
                default:
                    correct = false;
                    break;
            }

            Assert.True(correct);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetByIdTest(int id)
        {
            var result = await _repo.GetById(id);
            Assert.True(result is CommentDom);
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
        [InlineData("Comment5")]
        public async void AddTest(string body)
        {
            var commentDom = new CommentDom
            {
                PostId = 1,
                Body = body,
                User = new()
                {
                    FirstName = "Админ",
                    LastName = "Админович",
                    Patronymic = "Админов",
                    NickName = "Admin"
                },
                Date = DateTimeOffset.Now,
            };
            var result = await _repo.Add(commentDom);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(commentDom.Body, result.Body);
        }

        [Theory]
        [InlineData(1, "CommentUser", new string[3] { "Comment6", "Comment7", "Comment8" })]
        public async void AddRangeTest(int commentedPostId, string name, string[] bodyes)
        {
            var commentsDom = new CommentDom[3];
            for (int i = 0; i < commentsDom.Length; i++)
            {
                commentsDom[i] = new CommentDom
                {
                    PostId = commentedPostId,
                    User = new()
                    {
                        FirstName = name,
                        LastName = name,
                        Patronymic = name,
                        NickName = name
                    },
                    Body = bodyes[i],
                    Date = DateTimeOffset.Now,
                };
            }

            await _repo.AddRange(commentsDom);

            Assert.NotNull(_fixture.DB.Comments.FirstOrDefault(i => i.Body == bodyes[0]));
            Assert.NotNull(_fixture.DB.Comments.FirstOrDefault(i => i.Body == bodyes[1]));
            Assert.NotNull(_fixture.DB.Comments.FirstOrDefault(i => i.Body == bodyes[2]));
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

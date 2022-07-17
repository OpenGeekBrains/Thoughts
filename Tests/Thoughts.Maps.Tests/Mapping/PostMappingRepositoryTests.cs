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
                Assert.True(item is PostDom);
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
            var item = _mapper.Map(TestDbData.Posts.First());
            var result = await _repo.Exist(item);

            Assert.True(result);
        }

        [Theory]
        [InlineData(404, "Post404")]
        public async void ExistTest_ReturnFalse(int id, string title)
        {
            var item = _mapper.Map(TestDbData.Posts.First());
            item.Id = id;
            item.Title = title;

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
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is PostDom);
                    else if (result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is PostDom);
                    }
                    else correct = false;
                    break;
                case 2:
                    if (result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is PostDom);
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is PostDom);
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
            Assert.True(result is PostDom);
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData(404)]
        public async void GetById_ReturnNull(int id)
        {
            var result = await _repo.GetById(id);
            Assert.Null(result);
        }

        //[Theory]
        //[InlineData("Post5", "PostBody5")]
        //public async void AddTest(string title, string body)
        //{
        //    var postDom = _mapper.Map(TestDbData.Posts.First());
        //    postDom.Id = 0;
        //    postDom.Title = title;
        //    postDom.Body = body;

        //    var result = await _repo.Add(postDom);

        //    // ошибка
        //    // System.InvalidOperationException : The instance of entity type 'User' cannot be tracked because another instance with the key value
        //    // '{Id: 9e262546-d8ae-4ded-a969-499992dfbe81}' is already being tracked. When attaching existing entities, ensure that only one entity
        //    // instance with a given key value is attached.

        //    Assert.NotNull(result);
        //    Assert.NotEqual(0, result.Id);
        //    Assert.Equal(postDom.Body, result.Body);
        //}

        // todo
        // AddRangeTest()
        // GetPageTest()
        // UpdateTest()
        // UpdateRangeTest()
        // DeleteTest()
        // DeleteRangeTest()
        // DeleteByIdTest()
    }
}

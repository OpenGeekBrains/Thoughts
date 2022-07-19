using Thoughts.Maps.Tests.Fixtures;
using Thoughts.Services.Mapping;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using TagDAL = Thoughts.DAL.Entities.Tag;
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
    public class TagMappingRepositoryTests
    {
        DbFixture _fixture;
        MappingRepository<TagDAL, TagDom> _repo;
        DbRepository<TagDAL> _dbRepository;
        Mock<ILogger<DbRepository<TagDAL>>> _dbRepoMock;
        TagMapper _mapper;

        public TagMappingRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _mapper = new TagMapper();
            _dbRepoMock = new Mock<ILogger<DbRepository<TagDAL>>>();
            _dbRepository = new DbRepository<TagDAL>(_fixture.DB, _dbRepoMock.Object);
            _repo = new MappingRepository<TagDAL, TagDom>(_dbRepository, _mapper);
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
                Assert.True(item is TagDom);
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
            var item = _mapper.Map(TestDbData.Tags.First());
            var result = await _repo.Exist(item!);

            Assert.True(result);
        }

        [Theory]
        [InlineData(404, "Tag404")]
        public async void ExistTest_ReturnFalse(int id, string name)
        {
            var item = new TagDom { Id = id, Name = name };

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
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is TagDom);
                    else if (result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is TagDom);
                    }
                    else correct = false;
                    break;
                case 2:
                    if (result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is TagDom);
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is TagDom);
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
            Assert.True(result is TagDom);
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
            var tagDom = new TagDom()
            {
                Name = name,
            };

            var result = await _repo.Add(tagDom);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(tagDom.Name, result.Name);
        }

        [Fact]
        public async void AddRangeTest()
        {
            string[] names = new string[3] { "NewTag1", "NewTag2", "NewTag3" };
            var tagsDom = new TagDom[3];
            for (int i = 0; i < tagsDom.Length; i++)
            {
                tagsDom[i] = new TagDom
                {
                    Name = names[i],
                };
            }

            await _repo.AddRange(tagsDom);

            Assert.NotNull(_fixture.DB.Tags.FirstOrDefault(i => i.Name == names[0]));
            Assert.NotNull(_fixture.DB.Tags.FirstOrDefault(i => i.Name == names[1]));
            Assert.NotNull(_fixture.DB.Tags.FirstOrDefault(i => i.Name == names[2]));
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

using Thoughts.Maps.Tests.Fixtures;
using Thoughts.Services.Mapping;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CategoryDAL = Thoughts.DAL.Entities.Category;

using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Thoughts.Services.InSQL;
using Thoughts.Extensions.Maps;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        [InlineData(404)]
        public async void ExistIdTest_ReturnFalse(int id)
        {
            var result = await _repo.ExistId(id);
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, "Category1")]
        [InlineData(2, "Category2")]
        [InlineData(3, "Category3")]
        public async void ExistTest_ReturnTrue(int id, string name)
        {
            var item = new CategoryDom()
            {
                Id = id,
                Name = name,
            };

            var result = await _repo.Exist(item);

            Assert.True(result);
        }

        [Theory]
        [InlineData(404, "Category404")]
        public async void ExistTest_ReturnFalse(int id, string name)
        {
            var item = new CategoryDom()
            {
                Id = id,
                Name = name,
            };

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
            catch(ArgumentNullException e)
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
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is CategoryDom);
                    else if (result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is CategoryDom);
                    }
                    else correct = false;
                    break;
                case 2:
                    if(result.FirstOrDefault(s => s.Id == 2) is not null)
                    {
                        Assert.True(result.FirstOrDefault(s => s.Id == 3) is CategoryDom);
                        Assert.True(result.FirstOrDefault(s => s.Id == 2) is CategoryDom);
                    }
                    else correct = false;
                    break;
                default:
                    correct = false;
                    break;
            }

            Assert.True(correct);
        }


        // todo: GetPageTest()

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetByIdTest(int id)
        {
            var result = await _repo.GetById(id);
            Assert.True(result is CategoryDom);
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
        [InlineData("Category5")]
        public async void AddTest(string name)
        {
            var categoryDom = new CategoryDom
            {
                Name = name
            };
            var result = await _repo.Add(categoryDom);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(categoryDom.Name, result.Name);
        }

        [Theory]
        [InlineData(new int[3] { 6, 7, 8 }, new string[3] { "Category6", "Category7", "Category8" })]
        public async void AddRangeTest(int[] ids, string[] names)
        {
            var categoriesDom = new CategoryDom[3];
            for (int i = 0; i < categoriesDom.Length; i++)
            {
                categoriesDom[i] = new CategoryDom
                {
                    Id = ids[i],
                    Name = names[i],
                };
            }

            await _repo.AddRange(categoriesDom);

            Assert.NotNull(_fixture.DB.Categories.FirstOrDefault(i => i.Id == 6));
            Assert.NotNull(_fixture.DB.Categories.FirstOrDefault(i => i.Id == 7));
            Assert.NotNull(_fixture.DB.Categories.FirstOrDefault(i => i.Id == 8));
        }

        // todo
        //[Theory]
        //[InlineData("Category9", "Category99")]
        //public async void UpdateTest(string name, string updatedName)
        //{
        //    var categoryDom = new CategoryDom
        //    {
        //        Name = name,
        //    };

        //    var addedCategoryDom = await _repo.Add(categoryDom);
        //    var gettedCategoryDal = _fixture.DB.Categories.AsNoTracking().FirstOrDefault(i => i.Id == addedCategoryDom.Id);

        //    var gettedCategoryDom = _mapper.Map(gettedCategoryDal);

        //    gettedCategoryDom.Name = updatedName;

        //    var result = await _repo.Update(gettedCategoryDom);

        //    Assert.Equal(updatedName, result.Name);
        //    Assert.NotNull(_fixture.DB.Categories.FirstOrDefault(i => i.Name == updatedName));
        //}

        // todo: UpdateRangeTest()

        // todo
        //[Theory]
        //[InlineData("Category10")]
        //public async void DeleteTest(string name)
        //{
        //    var categoryDom = new CategoryDom
        //    {
        //        Name = name,
        //    };

        //    var addResult = await _repo.Add(categoryDom);

        //    var deleteResult = await _repo.Delete(addResult);

        //    Assert.NotNull(deleteResult);
        //    Assert.Null(_fixture.DB.Categories.FirstOrDefault(i => i.Id == addResult.Id));
        //}

        // todo : DeleteRangeTest()

        // todo : DeleteByIdTest()
    }
}

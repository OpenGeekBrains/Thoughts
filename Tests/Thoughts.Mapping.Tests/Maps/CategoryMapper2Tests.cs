using System.Collections.Generic;
using Thoughts.DAL.Entities;
using Thoughts.Extensions.Mapping.Maps2;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Mapping.Tests.Maps.Fixtures;
using Thoughts.Services.Data;
using Xunit;

using CategoryDom = Thoughts.Domain.Base.Entities.Category;
namespace Thoughts.Mapping.Tests.Maps
{
    public class CategoryMapper2Tests : IClassFixture<MemoizCashFixture>
    {
        private readonly IMapper<Category, CategoryDom> _mapper;
        ICollection<Category> Categories;

        public CategoryMapper2Tests(MemoizCashFixture cashFixture)
        {
            _mapper = new CategoryMapper2(cashFixture.Cash);
            Categories = TestDbData.Categories;

            int i = 1;
            foreach (var item in Categories)
            {
                item.Id = i++;
            }
        }

        [Fact]
        public void MapperCreated()
        {
            Assert.True(_mapper is not null);
        }

        
        [Fact]
        public void MapBackTest()
        {
            var testCollection = new List<CategoryDom>();
            foreach (var item in Categories)
            {
                testCollection.Add(_mapper.Map(item));
            }
            foreach (var item in testCollection)
            {
                Assert.True(item is CategoryDom);
            }
        }

        [Fact]
        public void MapTest()
        {
            var testCollection = new List<CategoryDom>();
            foreach (var item in Categories)
            {
                testCollection.Add(_mapper.Map(item));
            }
            foreach (var item in testCollection)
            {
                Assert.True(item is CategoryDom);
            }

            var resultCollection = new List<Category>();
            foreach (var item in testCollection)
            {
                resultCollection.Add(_mapper.MapBack(item));
            }
            foreach (var resultItem in resultCollection)
            {
                Assert.True(resultItem is Category);
            }
        }

    }
}

using Thoughts.Extensions.Mapping.Cash.DAL;
using Xunit;

namespace Thoughts.Mapping.Tests.Cash.DAL
{
    public class CategoryDalCashTests
    {
        CategoryDalCash _category = new CategoryDalCash();

        [Fact]
        public void CashCreated()
        {
            Assert.True(_category.Cash.Count == 0);
        }

        [Fact]
        public void CashDisposed()
        {
            _category.Dispose();
            Assert.True(_category.Cash is null);
        }
    }
}

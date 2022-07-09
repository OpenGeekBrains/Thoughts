using Thoughts.Extensions.Mapping.Cash.Domain;

using Xunit;

namespace Thoughts.Mapping.Tests.Cash.Domain
{
    public class CategoryDomainCashTests
    {
        CategoryDomainCash _category = new CategoryDomainCash();

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

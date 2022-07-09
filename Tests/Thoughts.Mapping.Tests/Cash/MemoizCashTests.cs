using System;

using Thoughts.Extensions.Mapping;

using Xunit;

namespace Thoughts.Mapping.Tests.Cash
{
    public class MemoizCashTests : IDisposable
    {
        MemoizCash _cash;

        public MemoizCashTests()
        {
            _cash = new MemoizCash();
        }
        public void Dispose()
        {
            _cash.Dispose();
        }

        [Fact]
        public void CashCreated()
        {
            var rolesDom = _cash.RolesDomain;
            Assert.NotNull(rolesDom);
            Assert.NotNull(_cash.RolesDomain);
        }
    }
}

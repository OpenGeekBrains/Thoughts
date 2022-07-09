using System;

using Thoughts.Extensions.Mapping;

namespace Thoughts.Mapping.Tests.Maps.Fixtures
{
    public class MemoizCashFixture : IDisposable
    {
        public MemoizCash Cash { get; private set; }
        public MemoizCashFixture()
        {
            Cash = new MemoizCash();
        }
        public void Dispose()
        {
            Cash?.Dispose();
        }
    }
}

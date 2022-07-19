using Xunit;

namespace Thoughts.Maps.Tests.Fixtures
{
    [CollectionDefinition("Database collection")]
    public class DbCollection : ICollectionFixture<DbFixture> { }
}

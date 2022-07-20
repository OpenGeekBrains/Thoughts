using Xunit;

namespace Thoughts.Services.NewTests.Fixtures
{
    [CollectionDefinition("Database collection")]
    public class DbCollection : ICollectionFixture<DbFixture> { }
}

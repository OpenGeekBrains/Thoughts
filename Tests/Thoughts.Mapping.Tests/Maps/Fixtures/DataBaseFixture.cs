using System;

using Microsoft.EntityFrameworkCore;

using Thoughts.DAL;

namespace Thoughts.Mapping.Tests.Maps.Fixtures
{
    public class DataBaseFixture : IDisposable
    {
        public ThoughtsDB DB { get; private set; }
        public DataBaseFixture()
        {
            var builder = new DbContextOptionsBuilder<ThoughtsDB>();
            builder.UseInMemoryDatabase("InMemoryDB");

            var options = builder.Options;
            DB = new ThoughtsDB(options);

            DB.Database.EnsureDeleted();
            DB.Database.EnsureCreated();
        }
        public void Dispose() => DB?.Dispose();
    }
}

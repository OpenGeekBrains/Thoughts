using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Thoughts.DAL;
using Thoughts.Services.Data;

namespace Thoughts.Maps.Tests.Fixtures
{
    public class DbFixture : IDisposable
    {
        public ThoughtsDB DB { get; private set; }
        public DbFixture()
        {
            var builder = new DbContextOptionsBuilder<ThoughtsDB>();
            builder.UseInMemoryDatabase("InMemoryDb");

            var options = builder.Options;

            DB = new ThoughtsDB(options);

            DB.Database.EnsureDeleted();
            DB.Database.EnsureCreated();

            DB.Categories.AddRange(TestDbData.Categories);
            DB.SaveChanges();
            if (!DB.Categories.Any())
            {
                throw new Exception("Categories Empty");
            }
        }

        public void Dispose()
        {
            DB?.Dispose();
        }
    }
}

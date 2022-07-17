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
            builder.EnableSensitiveDataLogging();

            var options = builder.Options;

            DB = new ThoughtsDB(options);

            DB.Database.EnsureDeleted();
            DB.Database.EnsureCreated();

            DB.Categories.AddRange(TestDbData.Categories);
            DB.Comments.AddRange(TestDbData.Comments);
            DB.Posts.AddRange(TestDbData.Posts);
            DB.SaveChanges();
            if (!DB.Categories.Any()) throw new Exception("Categories Set in DB is Empty");
            if (!DB.Comments.Any()) throw new Exception("Comments Set in DB is Empty");
            if (!DB.Posts.Any()) throw new Exception("Posts Set in DB is Empty");
        }

        public void Dispose()
        {
            DB?.Dispose();
        }
    }
}

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Thoughts.DAL;
using Thoughts.Services.Data;

namespace Thoughts.Services.NewTests.Fixtures
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
            DB.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            DB.Categories.AddRange(TestDbData.Categories);
            DB.Comments.AddRange(TestDbData.Comments);
            DB.Posts.AddRange(TestDbData.Posts);
            DB.Roles.AddRange(TestDbData.Roles);
            DB.Tags.AddRange(TestDbData.Tags);
            DB.Users.AddRange(TestDbData.Users);
            DB.SaveChanges();
            if (!DB.Categories.Any()) throw new Exception("Categories Set in DB is Empty");
            if (!DB.Comments.Any()) throw new Exception("Comments Set in DB is Empty");
            if (!DB.Posts.Any()) throw new Exception("Posts Set in DB is Empty");
            if (!DB.Roles.Any()) throw new Exception("Roles Set in DB is Empty");
            if (!DB.Tags.Any()) throw new Exception("Tags Set in DB is Empty");
            if (!DB.Users.Any()) throw new Exception("Users Set in DB is Empty");
        }

        public void Dispose()
        {
            DB?.Dispose();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thoughts.Extensions.Maps;
using Assert = Xunit.Assert;

using CategoryDomain = Thoughts.Domain.Base.Entities.Category;
using StatusDomain = Thoughts.Domain.Base.Entities.Status;
using CategoryDal = Thoughts.DAL.Entities.Category;
using StatusDal = Thoughts.DAL.Entities.Status;

namespace Thoughts.Tests.Services.ThoughtsExtensions.Maps
{
    [TestClass]
    public class MapsTests
    {
        [TestMethod]
        public void StatusDal_ToDomain_Tests()
        {
            var source = new StatusDal()
            {
                Id = 1,
                Name = "Test",
            };
            var result = source.ToDomain();

            Assert.True(result is StatusDomain);
            Assert.Equal(result.Id, source.Id);
            Assert.Equal(result.Name, source.Name);
        }
    }
}

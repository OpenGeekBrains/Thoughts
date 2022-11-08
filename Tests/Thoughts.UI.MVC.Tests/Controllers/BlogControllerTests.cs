using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Moq;

using Thoughts.DAL;
using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;
using Thoughts.UI.MVC.Controllers;
using Thoughts.UI.MVC.WebModels;

using Assert = Xunit.Assert;

namespace Thoughts.UI.MVC.Tests.Controllers;

[TestClass]
public class BlogControllerTests
{
    private Mock<IBlogPostManager> _BlogManagerMock = null!;
    private Mock<IConfiguration> _ConfigurationMock = null!;
    private Mock<IMapper> _MapperMock = null!;

    private ThoughtsDB _db = null!;

    private BlogController _Controller = null!;

    //[AssemblyInitialize]
    //public static void GlobalInitializer(TestContext Context)
    //{
        
    //}

    [TestInitialize]
    public void Initialize()
    {
        var post_manager_mock  = new Mock<IBlogPostManager>();
        var configuration_mock = new Mock<IConfiguration>();
        var mapper_mock        = new Mock<IMapper>();

        var config_mock = new ConfigurationBuilder()
           .AddInMemoryCollection(new Dictionary<string, string> { { "LengthTextOnHomeView", "5" } })
           .Build();

        configuration_mock.Setup(c => c.GetSection(It.IsAny<string>()))
           .Returns<string>(name => config_mock.GetSection(name));

        var db_opt = new DbContextOptionsBuilder<ThoughtsDB>()
           .UseInMemoryDatabase("Test-db")
           .Options;

        var db = new ThoughtsDB(db_opt);
        db.Database.EnsureCreated();

        var controller = new BlogController(
            post_manager_mock.Object,
            configuration_mock.Object,
            db,
            mapper_mock.Object);

        _Controller        = controller;
        _db                = db;
        _MapperMock        = mapper_mock;
        _ConfigurationMock = configuration_mock;
        _BlogManagerMock   = post_manager_mock;
    }

    //[TestCleanup]
    //public void Finalize()
    //{

    //}

    [TestMethod]
    public async Task Details_returns_with_View()
    {
        const int blog_post_id = 13;

        _BlogManagerMock.Setup(s => s.GetPostAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new Post
            {
               Id = blog_post_id
           });

        var result = await _Controller.Details(blog_post_id);

        var view_result = Assert.IsType<ViewResult>(result);

        Assert.Null(view_result.ViewName);

        var model = Assert.IsAssignableFrom<BlogDetailsWebModel>(view_result.Model);

        Assert.NotNull(model.Post);

        Assert.Equal(blog_post_id, model.Post.Id);

        _BlogManagerMock.Verify(s => s.GetPostAsync(It.Is<int>(v => v == blog_post_id), It.IsAny<CancellationToken>()));
        _BlogManagerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Details_returns_with_NotFount_when_post_not_exists()
    {
        const int blog_post_id = 13;

        _BlogManagerMock.Setup(s => s.GetPostAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync((Post?)null);

        var result = await _Controller.Details(blog_post_id);

        var action_result = Assert.IsType<NotFoundObjectResult>(result);

        Assert.NotNull(action_result.Value);

        var model = action_result.Value;

        var model_type     = model.GetType();
        var model_property = model_type.GetProperty("PostId");

        var actual_post_id = (int)model_property!.GetValue(model)!;

        Assert.Equal(blog_post_id, actual_post_id);
    }

}

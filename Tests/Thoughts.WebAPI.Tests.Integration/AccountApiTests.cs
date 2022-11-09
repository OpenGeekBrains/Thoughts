using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using Moq;

using Thoughts.DAL;
using Thoughts.Identity.DAL;
using Thoughts.Interfaces.Base.Repositories;
using Thoughts.Services.InSQL;
using Thoughts.WebAPI.Clients.Identity;

namespace Thoughts.WebAPI.Tests.Integration;

[TestClass]
public class AccountApiTests
{
    private WebApplicationFactory<Program> _WebAPIHostBuilder;
    private ILogger<AccountClient> _Logger;

    [TestInitialize]
    public void Initialize()
    {
        _WebAPIHostBuilder = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => builder
                   //.UseSetting("Database", "SqliteInMemory")
                   .UseSetting("DbRemoveBefore", "false")
            .ConfigureServices(services =>
                 {
                     services.RemoveAll<ThoughtsDB>();
                     services.RemoveAll<FileStorageDb>();
                     services.RemoveAll<IdentityDB>();

                     services.RemoveAll<DbContextOptions<ThoughtsDB>>();
                     services.RemoveAll<DbContextOptions<FileStorageDb>>();
                     services.RemoveAll<DbContextOptions<IdentityDB>>();

                     services.AddDbContext<ThoughtsDB>(opt => opt.UseInMemoryDatabase("MainDb")
                        .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                     services.AddDbContext<FileStorageDb>(opt => opt.UseInMemoryDatabase("FilesDb")
                        .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                     services.AddDbContext<IdentityDB>(opt => opt.UseInMemoryDatabase("IdentityDb")
                        .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

                 }))
            ;
        _Logger = new Mock<ILogger<AccountClient>>().Object;
    }

    [TestMethod]
    public async Task LoginFromService_GoodPasswordOrLogin()
    {
        var http_client = _WebAPIHostBuilder
           //.WithWebHostBuilder(builder => builder
           //    .ConfigureServices(services =>
           //     {
           //         services.RemoveAll(typeof(IRepository<>));
           //         services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
           //     }))
           .CreateClient();
        var account_client = new AccountClient(http_client, _Logger);

        await account_client.LoginAsync("Admin", "AdPAss_123");

        Assert.IsNotNull(account_client.Http.DefaultRequestHeaders.Authorization);
    
        var roles = await account_client.GetAllRolessAsync();

        Assert.IsNotNull(roles);

        var users = await account_client.GetAllUsersAsync();

        Assert.IsNotNull(users);
    }

    [TestMethod]
    [DataRow("Admin", "BadPassword")]
    [DataRow("BadLogin", "AdPAss_123")]
    [DataRow("BadLogin", "BadPassword")]
    public async Task LoginFromService_BadPasswordOrLogin(string login, string password)
    {
        var http_client = _WebAPIHostBuilder
           //.WithWebHostBuilder(builder => builder
           //    .ConfigureServices(services =>
           //     {
           //         services.RemoveAll(typeof(IRepository<>));
           //         services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
           //     }))
           .CreateClient();
        var account_client = new AccountClient(http_client, _Logger);

        // LoginAsync должен вернуть InvalidOperationException, но почему-то отлавливается только AggregateException
        Exception exception =  Assert.ThrowsException<AggregateException>( () =>
        {
            account_client.LoginAsync(login, password).Wait();

            Assert.IsNull(account_client.Http.DefaultRequestHeaders.Authorization);
        });

        Assert.AreEqual("One or more errors occurred. (Не удалось получить токен от сервера! Авторизация не выполнена.)", exception.Message);

        var roles = await account_client.GetAllRolessAsync();

        Assert.IsNull(roles);

        var users = await account_client.GetAllUsersAsync();

        Assert.IsNull(users);
    }
}

﻿using Thoughts.DAL;

namespace Thoughts.UI.MVC.Infrastructure.Extensions;

internal static class WebApplicationExtensions
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        var configuration = app.Configuration;

        await using var service_scope = app.Services.CreateAsyncScope();

        var services = service_scope.ServiceProvider;

        var db_initializer = services.GetRequiredService<ThoughtsDbInitializer>();

        await db_initializer.InitializeAsync(
            RemoveBefore: configuration.GetValue("DbRemoveBefore", false),
            InitializeTestData: configuration.GetValue("DbInitializeTestData", false));

        await using var fileDb = services.GetRequiredService<FileStorageDb>();

        if((await fileDb.Database.GetPendingMigrationsAsync()).Any())
            await fileDb.Database.MigrateAsync();
    }
}

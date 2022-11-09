using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Thoughts.DAL.Sqlite;
using Thoughts.DAL.SqlServer;
using Thoughts.Interfaces;
using Thoughts.Services.InSQL;
using Thoughts.WebAPI;
using Thoughts.WebAPI.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
services.AddControllers();

services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
});
services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.ConfigureOptions<SwaggerConfigureOptions>();

var db_type = configuration["Database"];

switch (db_type)
{
    default: throw new InvalidOperationException($"Тип БД {db_type} не поддерживается");

    case "Sqlite":
        services.AddThoughtsDbSqlite(configuration.GetConnectionString("Sqlite"));
        break;

    case "SqlServer":
        services.AddThoughtsDbSqlServer(configuration.GetConnectionString("SqlServer"));
        break;
}
services.AddScoped<IBlogPostManager, SqlBlogPostManager>();

builder.Services.AddTransient<ThoughtsDbInitializer>();

builder.Services.AddTransient<IShortUrlManager, SqlShortUrlManagerService>();

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

await app.InitializeDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    //endpoints.MapControllerRoute(
//    //    name: "default",
//    //    pattern: "{controller=Home}/{action=Index}/{id?}");
//});

app.Run();

public partial class Program { }

using System;
using System.Windows;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Thoughts.DAL;
using Thoughts.Interfaces.Base.Repositories;
using Thoughts.Services;
using Thoughts.Services.Data;
using Thoughts.Services.InSQL;
using Thoughts.UI.WPF.ViewModels;

namespace Thoughts.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost? __Hosting;
        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        public static IServiceProvider Services => Hosting.Services;        

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(opt => opt.AddJsonFile("appsettings.json", false, true)).ConfigureServices(ConfigureServices);
        
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<ThoughtsDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("SQLServer")));
            services.AddTransient<ThoughtsDbInitializer>();

            services.AddSingleton<FilesViewModel>();
            services.AddSingleton<PostsViewModel>();
            services.AddSingleton <MainWindowViewModel>();
            services.AddSingleton<UsersViewModel>();

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddSingleton<RepositoryBlogPostManager>();
            services.AddSingleton<TestDbData>();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var scope = Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<ThoughtsDbInitializer>();
                initializer.InitializeAsync().Wait();
            }

            base.OnStartup(e);
        }
    }
}

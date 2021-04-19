using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DocumentationLogicielle.App.Views;
using Microsoft.Extensions.DependencyInjection;


namespace DocumentationLogicielle.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AgileTeamsContext>(options => options.UseSqlServer("Server=Poulpe;Database=AgileTeams;Trusted_Connection=True;"));

            //services.AddSingleton<UsersServices>();
            //services.AddSingleton<TeamsServices>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<Login>();
            //services.AddSingleton<SignOnWindow>();
            //services.AddSingleton<ListingActionsWindow>();
            //services.AddSingleton<ProfileWindow>();
            //services.AddSingleton<CreateTeamWindow>();
            //services.AddSingleton<JoinTeamWindow>();
            //services.AddSingleton<MyTeamsWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}

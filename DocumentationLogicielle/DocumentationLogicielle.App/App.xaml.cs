using System.Windows;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;
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
            services.AddSingleton<ProjectDatabase>();
            services.AddSingleton<UserServices>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<BoardWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}

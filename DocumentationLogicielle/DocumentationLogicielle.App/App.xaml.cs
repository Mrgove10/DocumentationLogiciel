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
        
        /// <summary>
        /// Main class of the app
        /// </summary>
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Configures the services 
        /// </summary>
        /// <param name="services">Collection of services that needs to be configured</param>
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ProjectDatabase>();

            services.AddSingleton<UserServices>();
            services.AddSingleton<AlertServices>();
            services.AddSingleton<MaterialServices>();
            services.AddSingleton<ProductServices>();
            services.AddSingleton<MaterialsProductServices>();
            services.AddSingleton<SaleServices>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<BoardWindow>();
            services.AddSingleton<AddUserWindow>();
            services.AddSingleton<AlertsWindow>();
            services.AddSingleton<ListingElementsWindow>();
            services.AddSingleton<StatisticsWindow>();
            services.AddSingleton<UpdateStockWindow>();
            services.AddSingleton<AddElementWindow>();
        }

        /// <summary>
        /// Function that is launched on the startup of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}

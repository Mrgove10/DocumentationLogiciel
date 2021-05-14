using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    /// <summary>
    /// View model for the page "Board"
    /// </summary>
    public class BoardViewModel : IViewModel<BoardWindow, ICommand>
    {
        #region Commands
        
        public ICommand GoBackCommand { get; }

        /// <summary>
        /// Command to go to the page "AddUser"
        /// </summary>
        public ICommand GoToAddUserCommand { get; }

        /// <summary>
        /// Command to go to the page "Alerts"
        /// </summary>
        public IAsyncCommand GoToAlertsCommand { get; }

        /// <summary>
        /// Command to go to the page "ListingElements"
        /// </summary>
        public IAsyncCommand GoToListingElementsCommand { get; }

        /// <summary>
        /// Command to go to the page "Statistics"
        /// </summary>
        public IAsyncCommand GoToStatisticsCommand { get; }

        /// <summary>
        /// Command to go to the page "UpdateStock"
        /// </summary>
        public IAsyncCommand GoToUpdateStockCommand { get; }

        /// <summary>
        /// Command to go to the page "AddElement"
        /// </summary>
        public IAsyncCommand GoToAddElementCommand { get; }

        #endregion

        /// <summary>
        /// The name of the current user
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Number of alerts
        /// </summary>
        public int BadgeAlert { get; set; }

        #region Visibility of the modules in the interface

        /// <summary>
        /// Property to display only modules for the admins
        /// </summary>
        public Visibility CurrentUserAdmin { get; set; }

        /// <summary>
        /// Property to display only modules for the users
        /// </summary>
        public Visibility CurrentUserLambda { get; set; }

        #endregion

        public BoardWindow CurrentPage { get; set; }

        #region Services

        /// <summary>
        /// Services to interact with the table "User" (<see cref="User"/>)
        /// </summary>
        public UserServices UserServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Alert" (<see cref="Alert"/>)
        /// </summary>
        public AlertServices AlertServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Material" (<see cref="Material"/>)
        /// </summary>
        public MaterialServices MaterialServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Material" (<see cref="Product"/>)
        /// </summary>
        public ProductServices ProductServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Material" (<see cref="MaterialsProduct"/>)
        /// </summary>
        public MaterialsProductServices MaterialsProductServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Material" (<see cref="Sale"/>)
        /// </summary>
        public SaleServices SaleServices { get; set; }


        #endregion

        /// <summary>
        /// Constructor of the view model
        /// </summary>
        /// <param name="currentPage">Page of the view mode</param>
        /// <param name="userServices">Services for the "User" table</param>
        /// <param name="alertServices">Services for the "Alert" table</param>
        /// <param name="materialServices">Services for the "Material" table</param>
        /// <param name="productServices">Services for the "Product" table</param>
        /// <param name="materialsProductServices">Services for the "MaterialProduct" table</param>
        /// <param name="saleServices">Services for the "Sale" table</param>
        /// <param name="countAlerts">Number of alerts</param>
        public BoardViewModel(BoardWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices, int countAlerts)
        {
            CurrentPage = currentPage;

            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";
            CurrentUserAdmin = AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? Visibility.Visible : Visibility.Hidden;
            CurrentUserLambda = AppSettings.CurrentUser.Role == ERole.User.ToString() ? Visibility.Visible : Visibility.Hidden;

            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;

            BadgeAlert = countAlerts;

            GoBackCommand = new CommandHandler(GoBack, () => true);
            GoToAddUserCommand = new CommandHandler(GoToAddUser, () => true);
            GoToAlertsCommand = new AsyncCommand(GoToAlerts, () => true);
            GoToListingElementsCommand = new AsyncCommand(GoToListingElements, () => true);
            GoToStatisticsCommand = new AsyncCommand(GoToStatistics, () => true);
            GoToUpdateStockCommand = new AsyncCommand(GoToUpdateStock, () => true);
            GoToAddElementCommand = new AsyncCommand(GoToAddElement, () => true);
        }

        /// <summary>
        /// Method to go back to the precedent page
        /// </summary>
        /// <param name="parameter"></param>
        private void GoBack(object parameter)
        {
            MainWindow page = new MainWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices);
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Go to the page to add an element
        /// </summary>
        /// <returns></returns>
        private async Task GoToAddElement()
        {
            AddElementWindow page = new AddElementWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await MaterialServices.GetAll());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to go to the "Alert" page
        /// </summary>
        /// <returns></returns>
        private async Task GoToAlerts()
        {
            AlertsWindow page = new AlertsWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await AlertServices.GetAllAlerts());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Go to the page to update stock of element
        /// </summary>
        /// <returns></returns>
        private async Task GoToUpdateStock()
        {
            UpdateStockWindow page = new UpdateStockWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await ProductServices.GetAll(), await MaterialServices.GetAll());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to go to the "Listing" page
        /// </summary>
        /// <returns></returns>
        private async Task GoToListingElements()
        {
            ListingElementsWindow page = new ListingElementsWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await ProductServices.GetAll(), await MaterialServices.GetAll(), await MaterialsProductServices.GetAll());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to go to the "Statistic" page
        /// </summary>
        /// <returns></returns>
        private async Task GoToStatistics()
        {
            StatisticsWindow page = new StatisticsWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await SaleServices.CountBySite(), await SaleServices.EvolutionByMonth(), await SaleServices.TotalMoneyEarn(), await SaleServices.MoneyByYear());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to go to the "AddUser" page
        /// </summary>
        /// <param name="parameter"></param>
        private void GoToAddUser(object parameter)
        {
            AddUserWindow page = new AddUserWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices);
            page.Show();
            CurrentPage.Close();
        }
    }
}

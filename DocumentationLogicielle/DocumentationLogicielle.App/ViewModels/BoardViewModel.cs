using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    public class BoardViewModel
    {
        #region Commands

        /// <summary>
        /// Command to go back to the precedent page
        /// </summary>
        public ICommand GoBackCommand { get; }

        /// <summary>
        /// Command to go to the page "AddUser"
        /// </summary>
        public ICommand GoToAddUserCommand { get; }

        /// <summary>
        /// Command to go to the page "Alerts"
        /// </summary>
        public IAsyncCommand GoToAlertsCommand { get; }

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

        /// <summary>
        /// Property which correspond to the current page
        /// </summary>
        public BoardWindow CurrentPage { get; set; }

        /// <summary>
        /// Services to interact with the table "User" (<see cref="User"/>)
        /// </summary>
        public UserServices UserServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Alert" (<see cref="Alert"/>)
        /// </summary>
        public AlertServices AlertServices { get; set; }

        public BoardViewModel(BoardWindow currentPage, UserServices userServices, AlertServices alertServices, int countAlerts)
        {
            CurrentPage = currentPage;

            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";
            CurrentUserAdmin = AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? Visibility.Visible : Visibility.Hidden;
            CurrentUserLambda = AppSettings.CurrentUser.Role == ERole.User.ToString() ? Visibility.Visible : Visibility.Hidden;

            UserServices = userServices;
            AlertServices = alertServices;

            BadgeAlert = countAlerts;

            GoBackCommand = new CommandHandler(GoBack, () => true);
            GoToAddUserCommand = new CommandHandler(GoToAddUser, () => true);
            GoToAlertsCommand = new AsyncCommand(GoToAlerts, () => true);
        }

        /// <summary>
        /// Method to go back to the precedent page
        /// </summary>
        /// <param name="parameter"></param>
        private void GoBack(object parameter)
        {
            MainWindow page = new MainWindow(UserServices, AlertServices);
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to go to the "Alert" page
        /// </summary>
        /// <returns></returns>
        private async Task GoToAlerts()
        {
            AlertsWindow page = new AlertsWindow(UserServices, AlertServices, await AlertServices.GetAllAlerts());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to go to the "AddUser" page
        /// </summary>
        /// <param name="parameter"></param>
        private void GoToAddUser(object parameter)
        {
            AddUserWindow page = new AddUserWindow(UserServices, AlertServices);
            page.Show();
            CurrentPage.Close();
        }
    }
}

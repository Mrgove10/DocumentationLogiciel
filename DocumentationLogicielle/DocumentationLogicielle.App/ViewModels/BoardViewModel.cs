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
        public ICommand GoBackCommand { get; }
        public ICommand GoToAddUserCommand { get; }
        public IAsyncCommand GoToAlertsCommand { get; }

        public string CurrentUserName { get; set; }
        public int BadgeAlert { get; set; }

        public Visibility CurrentUserAdmin { get; set; }
        public Visibility CurrentUserLambda { get; set; }

        public BoardWindow CurrentPage { get; set; }
        public UserServices UserServices { get; set; }
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
        private void GoBack(object parameter)
        {
            MainWindow page = new MainWindow(UserServices, AlertServices);
            page.Show();
            CurrentPage.Close();
        }

        private async Task GoToAlerts()
        {
            AlertsWindow page = new AlertsWindow(UserServices, AlertServices, await AlertServices.GetAllAlerts());
            page.Show();
            CurrentPage.Close();
        }

        private void GoToAddUser(object parameter)
        {
            AddUserWindow page = new AddUserWindow(UserServices, AlertServices);
            page.Show();
            CurrentPage.Close();
        }
    }
}

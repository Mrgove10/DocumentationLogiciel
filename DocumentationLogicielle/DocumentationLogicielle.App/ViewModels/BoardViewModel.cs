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

        public string CurrentUserName { get; set; }

        public Visibility CurrentUserAdmin { get; set; }
        public Visibility CurrentUserLambda { get; set; }

        public BoardWindow CurrentPage { get; set; }
        public UserServices UserServices { get; set; }

        public BoardViewModel(BoardWindow currentPage, UserServices userServices)
        {
            CurrentPage = currentPage;
            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";
            CurrentUserAdmin = AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? Visibility.Visible : Visibility.Hidden;
            CurrentUserLambda = AppSettings.CurrentUser.Role == ERole.User.ToString() ? Visibility.Visible : Visibility.Hidden;
            UserServices = userServices;

            GoBackCommand = new CommandHandler(GoBack, () => true);
            GoToAddUserCommand = new CommandHandler(GoToAddUser, () => true);
        }
        private void GoBack(object parameter)
        {
            MainWindow page = new MainWindow(UserServices);
            page.Show();
            CurrentPage.Close();
        }

        private void GoToAddUser(object parameter)
        {
            AddUserWindow page = new AddUserWindow(UserServices);
            page.Show();
            CurrentPage.Close();
        }
    }
}

using System.Windows.Input;
using DocumentationLogicielle.App.Views;

namespace DocumentationLogicielle.App.ViewModels
{
    public class LoginViewModel
    {
        #region Commands

        /// <summary>
        /// Commande pour revenir en arrière d'une page
        /// </summary>
        public ICommand GoBackCommand { get; }
        public ICommand LogInCommand { get; }

        #endregion

        public LoginWindow CurrentPage { get; set; }

        public LoginViewModel(LoginWindow currentPage)
        {
            CurrentPage = currentPage;

            GoBackCommand = new CommandHandler(GoBack, () => true);
            //LogInCommand = new CommandHandler(LogIn, CanLogIn);
        }

        private void GoBack(object parameter)
        {
            MainWindow page = new MainWindow();
            page.Show();
            CurrentPage.Close();
        }

    }
}

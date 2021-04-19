using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentationLogicielle.App.Views;

namespace DocumentationLogicielle.App.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand ExitCommand { get; }
        public ICommand SignOnCommand { get; }
        public ICommand LogInCommand { get; }

        public MainWindow CurrentPage { get; set; }

        public MainWindowViewModel(MainWindow currentPage)
        {
            CurrentPage = currentPage;

            ExitCommand = new CommandHandler(Exit, () => true);
            SignOnCommand = new CommandHandler(NavigateToSignOnPage, () => true);
            LogInCommand = new CommandHandler(NavigateToLogInPage, () => true);
        }

        /// <summary>
        /// Sortir de l'application
        /// </summary>
        /// <param name="parameter"></param>
        private void Exit(object parameter)
        {
            Application.Current.Shutdown();
        }

        private void NavigateToSignOnPage(object parameter)
        {
            //SignOnWindow page = new SignOnWindow(UsersServices, TeamsServices);
            //page.Show();
            //CurrentPage.Close();
        }

        private void NavigateToLogInPage(object parameter)
        {
            Login page = new Login();
            page.Show();
            CurrentPage.Close();
        }

    }
}

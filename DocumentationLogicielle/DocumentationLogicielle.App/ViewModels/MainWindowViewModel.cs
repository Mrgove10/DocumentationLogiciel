using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentationLogicielle.App.Models;
using DocumentationLogicielle.App.Services;
using DocumentationLogicielle.App.Views;

namespace DocumentationLogicielle.App.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private User user;

        public ICommand ExitCommand { get; }
        public IAsyncCommand LogInCommand { get; }

        #region Inputs

        public string LoginInput
        {
            get => user.Login;
            set
            {
                user.Login = value;
                IsLoginOk = !string.IsNullOrEmpty(value);
                IsButtonOk = true;
                OnPropertyChange();
                OnPropertyChange(nameof(IsLoginOk));
                OnPropertyChange(nameof(IsButtonOk));
                OnPropertyChange(nameof(LoginValidation));
                OnPropertyChange(nameof(ButtonValidation));
            }
        }

        public string PasswordInput
        {
            get => user.Password;
            set
            {
                user.Password = value;
                IsPasswordOk = !string.IsNullOrEmpty(value);
                IsButtonOk = true;
                OnPropertyChange();
                OnPropertyChange(nameof(IsPasswordOk));
                OnPropertyChange(nameof(IsButtonOk));
                OnPropertyChange(nameof(PasswordValidation));
                OnPropertyChange(nameof(ButtonValidation));
            }
        }

        #endregion

        #region Validations

        public bool IsLoginOk { get; set; }
        public bool IsPasswordOk { get; set; }
        public bool IsButtonOk { get; set; } = true;

        public string LoginValidation => IsLoginOk ? "" : "Login field can't be empty";

        public string PasswordValidation => IsPasswordOk ? "" : "Please enter a value";

        public string ButtonValidation => IsButtonOk ? "" : "Wrong Password or login";

        #endregion


        public MainWindow CurrentPage { get; set; }

        public UserServices UserServices { get; set; }

        public MainWindowViewModel(MainWindow currentPage, UserServices userServices)
        {
            CurrentPage = currentPage;
            UserServices = userServices;

            user = new User
            {
                Login = string.Empty,
                Password = string.Empty
            };
            
            ExitCommand = new CommandHandler(Exit, () => true);
            LogInCommand = new AsyncCommand(LogIn, CanLogIn);
        }

        #region Property Changes

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        /// <summary>
        /// Sortir de l'application
        /// </summary>
        /// <param name="parameter"></param>
        private void Exit(object parameter)
        {
            Application.Current.Shutdown();
        }

        private async Task LogIn()
        {
            IsButtonOk = await UserServices.IsUserExists(LoginInput, PasswordInput);
            if (IsButtonOk)
            {
                AppSettings.CurrentUser = await UserServices.GetUser(LoginInput, PasswordInput);
                BoardWindow page = new BoardWindow();
                page.Show();
                CurrentPage.Close();
            }
            else
            {
                OnPropertyChange(nameof(IsButtonOk));
                OnPropertyChange(nameof(ButtonValidation));
            }
        }

        private bool CanLogIn()
        {
            return IsPasswordOk && IsLoginOk;
        }
    }
}

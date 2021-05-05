using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentationLogicielle.App.Rules;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Model of a user to login
        /// See <see cref="User"/> 
        /// </summary>
        private User user;

        #region Command

        /// <summary>
        /// Command to exit of the application
        /// </summary>
        public ICommand ExitCommand { get; }

        /// <summary>
        /// Asynchronous command to log in the application
        /// </summary>
        public IAsyncCommand LogInCommand { get; }

        #endregion

        #region Inputs

        /// <summary>
        /// Represents the input of the login of the user
        /// The "set" has a personal implementation
        /// </summary>
        public string LoginInput
        {
            get => user.Login;
            set
            {
                // check if the value is empty or not
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

        /// <summary>
        /// Represents the input of the password of the user
        /// The "set" has a personal implementation
        /// </summary>
        public string PasswordInput
        {
            get => user.Password;
            set
            {
                // check if the value is empty or not
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

        /// <summary>
        /// Boolean for the state of the login of the user
        /// </summary>
        public bool IsLoginOk { get; set; }

        /// <summary>
        /// Boolean for the state of the password of the user
        /// </summary>
        public bool IsPasswordOk { get; set; }

        /// <summary>
        /// Boolean for the state of the button login
        /// </summary>
        public bool IsButtonOk { get; set; } = true;

        /// <summary>
        /// Personal field for the validation of the login of the user
        /// <remarks><b>This field is not display at the user anymore, we use intern validation : see <see cref="NotNullValidationRule"/></b></remarks>
        /// </summary>
        public string LoginValidation => IsLoginOk ? "" : "Login field can't be empty";

        /// <summary>
        /// Personal field for the validation of the password of the user
        /// <remarks><b>This field is display at the user</b></remarks>
        /// </summary>
        public string PasswordValidation => IsPasswordOk ? "" : "Please enter a value";

        /// <summary>
        /// Personal field for the validation when the user click on the button "Login"
        /// <remarks><b>This field is display at the user</b></remarks>
        /// </summary>
        public string ButtonValidation => IsButtonOk ? "" : "Wrong Password or login";

        #endregion

        /// <summary>
        /// The page that correspond to the display window
        /// </summary>
        public MainWindow CurrentPage { get; set; }

        /// <summary>
        /// Services to interact with the table "User" (model : <see cref="User"/>) of the database
        /// </summary>
        public UserServices UserServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Alert" (model : <see cref="Alert"/>) of the database
        /// </summary>
        public AlertServices AlertServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }

        public MainWindowViewModel(MainWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices)
        {
            CurrentPage = currentPage;
            
            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;

            user = new User
            {
                Login = string.Empty,
                Password = string.Empty,
                Role = string.Empty
            };
            
            ExitCommand = new CommandHandler(Exit, () => true);
            LogInCommand = new AsyncCommand(LogIn, CanLogIn);
        }

        #region Property Changes

        /// <summary>
        /// Property which allow to update a field
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Specific method to update a field without refreshing
        /// </summary>
        /// <param name="propertyName">Name of the field</param>
        protected void OnPropertyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        /// <summary>
        /// Quit the application
        /// </summary>
        /// <param name="parameter"></param>
        private void Exit(object parameter)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Check if the user exists in the database the connect it and save his info the storage
        /// <remarks>If the user do not exists in the database, display an error message</remarks>
        /// </summary>
        /// <returns></returns>
        private async Task LogIn()
        {
            IsButtonOk = await UserServices.IsUserExists(LoginInput, PasswordInput);
            if (IsButtonOk)
            {
                AppSettings.CurrentUser = await UserServices.GetUser(LoginInput, PasswordInput);
                BoardWindow page = new BoardWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, await AlertServices.CountAlerts());
                page.Show();
                CurrentPage.Close();
            }
            else
            {
                OnPropertyChange(nameof(IsButtonOk));
                OnPropertyChange(nameof(ButtonValidation));
            }
        }

        /// <summary>
        /// Check if the user can click on the button login
        /// </summary>
        /// <returns></returns>
        private bool CanLogIn()
        {
            return IsPasswordOk && IsLoginOk;
        }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using DocumentationLogicielle.App.Rules;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    /// <summary>
    /// View model for the page "AddUser"
    /// </summary>
    public class AddUserViewModel : INotifyPropertyChanged, IViewModel<AddUserWindow, IAsyncCommand>
    {
        #region Private properties

        /// <summary>
        /// Private property for user
        /// </summary>
        private User user;

        #endregion

        #region Commands

        /// <summary>
        /// Command to go back to the precedent page
        /// </summary>
        public IAsyncCommand GoBackCommand { get; }

        /// <summary>
        /// Command to add a user
        /// </summary>
        public ICommand AddUserCommand { get; }

        #endregion

        /// <summary>
        /// The name of the current user
        /// </summary>
        public string CurrentUserName { get; set; }

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
        /// Property which correspond to the current page <see cref="AddUserWindow"/>
        /// </summary>
        public AddUserWindow CurrentPage { get; set; }
        

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
        /// Constructor for the view model
        /// </summary>
        /// <param name="currentPage">Current page for this view model</param>
        /// <param name="userServices">Services for the "User" table</param>
        /// <param name="alertServices">Services for the "Alert" table</param>
        /// <param name="materialServices">Services for the "Material" table</param>
        /// <param name="productServices">Services for the "Product" table</param>
        /// <param name="materialsProductServices">Services for the "MaterialProduct" table</param>
        /// <param name="saleServices">Services for the "Sale" table</param>
        public AddUserViewModel(AddUserWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices)
        {
            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";
            
            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;

            user = new User
            {
                Login = string.Empty,
                Password = string.Empty,
                Role = ERole.User.ToString()
            };

            CurrentPage = currentPage;
            GoBackCommand = new AsyncCommand(GoBack, () => true);
            AddUserCommand = new CommandHandler(Create, CanCreate);
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
        /// Method to go back to the precedent page
        /// </summary>
        public async Task GoBack()
        {
            BoardWindow page = new BoardWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await AlertServices.CountAlerts());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to create a user
        /// </summary>
        /// <param name="parameter">Potential parameter passed to the command</param>
        private void Create(object parameter)
        {
            try
            {
                UserServices.CreateUser(LoginInput, PasswordInput, CurrentPage.RoleComboBox.Text);

                if (CurrentPage.UserAddSnackbar.MessageQueue is { } messageQueue)
                {
                    var message = $"User '{LoginInput}' has been add !";
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                }

                LoginInput = string.Empty;
                PasswordInput = string.Empty;
                CurrentPage.PasswordBoxPerso.Password = "";
            }
            catch (Exception e)
            {
                if (CurrentPage.UserAddSnackbar.MessageQueue is { } messageQueue)
                {
                    var message = $"A problem occurs, please retry or contact the support.";
                    CurrentPage.UserAddSnackbar.Background = Brushes.Red;
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                }
            }
        }

        /// <summary>
        /// Check if the user can click on the button login
        /// </summary>
        /// <returns></returns>
        private bool CanCreate()
        {
            return IsPasswordOk && IsLoginOk;
        }

    }
}

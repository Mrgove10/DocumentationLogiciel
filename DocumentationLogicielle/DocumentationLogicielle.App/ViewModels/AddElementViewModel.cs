using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    public class AddElementViewModel : IViewModel<AddElementWindow, IAsyncCommand>, INotifyPropertyChanged
    {
        #region Private properties

        private Visibility displayProduct;
        private Visibility displayMaterial;
        private Visibility buttonDisplay;

        #endregion

        #region Commands

        public IAsyncCommand GoBackCommand { get; }
        public ICommand AddElementCommand { get; }

        #endregion

        public Visibility DisplayProduct
        {
            get => displayProduct;
            set
            {
                displayProduct = value;
                OnPropertyChange();
            }
        }

        public Visibility DisplayMaterial
        {
            get => displayMaterial;
            set
            {
                displayMaterial = value;
                OnPropertyChange();
            }
        }
        
        public Visibility ButtonDisplay
        {
            get => buttonDisplay;
            set
            {
                buttonDisplay = value;
                OnPropertyChange();
            }
        }

        public string CurrentUserName { get; set; }
        public AddElementWindow CurrentPage { get; set; }

        #region Services

        public UserServices UserServices { get; set; }
        public AlertServices AlertServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }
        public SaleServices SaleServices { get; set; }

        #endregion

        public AddElementViewModel(AddElementWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices)
        {
            CurrentPage = currentPage;

            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";

            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;

            DisplayProduct = Visibility.Hidden;
            DisplayMaterial = Visibility.Hidden;
            ButtonDisplay = Visibility.Hidden;

            GoBackCommand = new AsyncCommand(GoBack, () => true);
            AddElementCommand = new CommandHandler(CreateElement, () => true);
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

        public void GenerateForm()
        {
            if (CurrentPage.TypeElementComboBox.SelectedItem != null)
            {
                var itemSelected = ((ComboBoxItem)CurrentPage.TypeElementComboBox.SelectedItem).Content;
                switch (itemSelected.ToString())
                {
                    case "Material":
                        DisplayProduct = Visibility.Hidden;
                        DisplayMaterial = Visibility.Visible;
                        break;
                    case "Product":
                        DisplayProduct = Visibility.Visible;
                        DisplayMaterial = Visibility.Hidden;
                        break;
                }

                ButtonDisplay = Visibility.Visible;
            }
        }

        public void CreateElement(object parameter)
        {
            if (CurrentPage.ElementAddSnackbar.MessageQueue is { } messageQueue)
            {
                var message = $"Element of ... has been changed !";
                Task.Factory.StartNew(() => messageQueue.Enqueue(message)).Wait();
            }

            DisplayProduct = Visibility.Hidden;
            DisplayMaterial = Visibility.Hidden;
            ButtonDisplay = Visibility.Hidden;
            CurrentPage.TypeElementComboBox.SelectedItem = null;
        }

        /// <summary>
        /// Method to go back to the precedent page
        /// </summary>
        /// <returns></returns>
        private async Task GoBack()
        {
            BoardWindow page = new BoardWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await AlertServices.CountAlerts());
            page.Show();
            CurrentPage.Close();
        }
    }
}

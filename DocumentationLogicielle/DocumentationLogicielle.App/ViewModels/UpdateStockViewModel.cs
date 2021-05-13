using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DocumentationLogicielle.App.Templates;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    /// <summary>
    /// TODO => pas besoin de commenter les éléments qui viennent de l'interface IViewModel : ils sont déjà commentés dans l'interface
    /// </summary>
    public class UpdateStockViewModel : IViewModel<UpdateStockWindow, IAsyncCommand>, INotifyPropertyChanged
    {
        private StockTemplate stockTemplate;
        private Visibility displayForm;

        #region Commands

        public IAsyncCommand GoBackCommand { get; }
        public IAsyncCommand UpdateProductCommand { get; }

        #endregion

        #region Inputs

        public int ProductStock
        {
            get => stockTemplate.Stock;
            set
            {
                stockTemplate.Stock = value;
                OnPropertyChange();
            }
        }

        public float ProductPrice
        {
            get => stockTemplate.Price;
            set
            {
                stockTemplate.Price = value;
                OnPropertyChange();
            }
        }


        #endregion

        #region Validations

        /// <summary>
        /// Boolean for the state of the login of the user
        /// </summary>
        public bool IsStockOk { get; set; }

        /// <summary>
        /// Boolean for the state of the password of the user
        /// </summary>
        public bool IsPriceOk { get; set; }

        /// <summary>
        /// Boolean for the state of the button login
        /// </summary>
        public bool IsButtonOk { get; set; } = true;

        #endregion

        public Visibility DisplayForm
        {
            get => displayForm;
            set
            {
                displayForm = value;
                OnPropertyChange();
            }
        }

        public string CurrentUserName { get; set; }
        public UpdateStockWindow CurrentPage { get; set; }

        #region Services

        public UserServices UserServices { get; set; }
        public AlertServices AlertServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }
        public SaleServices SaleServices { get; set; }

        #endregion

        public UpdateStockViewModel(UpdateStockWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices, List<Product> products, List<Material> materials)
        {
            CurrentPage = currentPage;

            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";

            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;

            stockTemplate = new StockTemplate();

            GenerateProductsComboBox(products, materials);

            GoBackCommand = new AsyncCommand(GoBack, () => true);
            UpdateProductCommand = new AsyncCommand(UpdateElement, () => true);
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

        public void GenerateProductsComboBox(List<Product> products, List<Material> materials)
        {
            var list = products.Select(x => x.Label).ToList();
            list.AddRange(materials.Select(x => x.Label).ToList());
            
            CurrentPage.ProductsComboBox.ItemsSource = list;
            CurrentPage.GridForm.Visibility = Visibility.Hidden;
        }

        public async Task GenerateForm()
        {
            var itemSelected = CurrentPage.ProductsComboBox.SelectedItem;
            if (itemSelected != null)
            {
                var product = await ProductServices.GetByLabel(itemSelected.ToString());
                var material = await MaterialServices.GetByLabel(itemSelected.ToString());

                if (product != null)
                {
                    ProductStock = product.Quantity;
                    ProductPrice = product.Price;
                }
                else if (material != null)
                {
                    ProductStock = material.Quantity;
                    ProductPrice = material.Price;
                }

                CurrentPage.GridForm.Visibility = Visibility.Visible;
            }
            else
            {
                CurrentPage.GridForm.Visibility = Visibility.Hidden;
            }
            
        }

        public async Task UpdateElement()
        {
            try
            {
                var itemSelected = CurrentPage.ProductsComboBox.SelectedItem;
                var product = await ProductServices.GetByLabel(itemSelected.ToString());
                var material = await MaterialServices.GetByLabel(itemSelected.ToString());

                if (product != null)
                {
                    product.Quantity = ProductStock;
                    product.Price = ProductPrice;
                    ProductServices.Update(product);
                }
                else if (material != null)
                {
                    material.Quantity = ProductStock;
                    material.Price = ProductPrice;
                    MaterialServices.Update(material);
                }

                if (CurrentPage.StockUpdateSnackbar.MessageQueue is { } messageQueue)
                {
                    var message = $"Stock/price of '{itemSelected}' has been changed !";
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message)).Wait();
                }

                CurrentPage.GridForm.Visibility = Visibility.Hidden;
                CurrentPage.ProductsComboBox.SelectedItem = null;
            }
            catch (Exception e)
            {
                if (CurrentPage.StockUpdateSnackbar.MessageQueue is { } messageQueue)
                {
                    var message = $"A problem occurs, please retry or contact the support.";
                    CurrentPage.StockUpdateSnackbar.Background = Brushes.Red;
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message)).Wait();
                }
            }
            
        }

        public async Task GoBack()
        {
            BoardWindow page = new BoardWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await AlertServices.CountAlerts());
            page.Show();
            CurrentPage.Close();
        }
    }
}

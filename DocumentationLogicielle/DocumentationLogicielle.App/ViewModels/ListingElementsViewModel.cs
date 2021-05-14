using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using DocumentationLogicielle.App.Templates;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    /// <summary>
    /// View model for the page "ListingElement"
    /// </summary>
    public class ListingElementsViewModel : IViewModel<ListingElementsWindow, IAsyncCommand>
    {
        #region Commands

        /// <summary>
        /// Command to go back to the precedent page
        /// </summary>
        public IAsyncCommand GoBackCommand { get; }

        #endregion

        /// <summary>
        /// Name of the current user
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// List of products
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// List of materials
        /// </summary>
        public List<Material> Materials { get; set; }

        /// <summary>
        /// List of materials of product
        /// </summary>
        public List<MaterialsProduct> MaterialsProducts { get; set; }

        /// <summary>
        /// Correspond to the current page
        /// </summary>
        public ListingElementsWindow CurrentPage { get; set; }

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
        /// Constructor of the view model
        /// </summary>
        /// <param name="currentPage">Page of the view mode</param>
        /// <param name="userServices">Services for the "User" table</param>
        /// <param name="alertServices">Services for the "Alert" table</param>
        /// <param name="materialServices">Services for the "Material" table</param>
        /// <param name="productServices">Services for the "Product" table</param>
        /// <param name="materialsProductServices">Services for the "MaterialProduct" table</param>
        /// <param name="saleServices">Services for the "Sale" table</param>
        /// <param name="products">List of products</param>
        /// <param name="materials">List of materials</param>
        /// <param name="materialsProducts">List of materials product</param>
        public ListingElementsViewModel(ListingElementsWindow currentPage, 
                                        UserServices userServices, 
                                        AlertServices alertServices, 
                                        MaterialServices materialServices, 
                                        ProductServices productServices, 
                                        MaterialsProductServices materialsProductServices,
                                        SaleServices saleServices,
                                        List<Product> products, 
                                        List<Material> materials, 
                                        List<MaterialsProduct> materialsProducts)
        {
            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";
            CurrentPage = currentPage;
            
            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;

            Products = products;
            Materials = materials;
            MaterialsProducts = materialsProducts;

            GenerateDatagrid();

            GoBackCommand = new AsyncCommand(GoBack, () => true);
        }

        /// <summary>
        /// Method to generate the datagrid of elements
        /// </summary>
        public void GenerateDatagrid()
        {
            CurrentPage.ListElements.ItemsSource = Convert();
        }

        /// <summary>
        /// Transform <see cref="Product"/> and <see cref="Material"/> into <see cref="ElementTemplate"/>
        /// </summary>
        /// <returns>Return a list of <see cref="ElementTemplate"/> based on products and materials</returns>
        private List<ElementTemplate> Convert()
        {
            List<ElementTemplate> elements = new List<ElementTemplate>();
            
            if (((ComboBoxItem)CurrentPage.FilterbyComboBox.SelectedItem).Content.Equals("Construction materials") || ((ComboBoxItem)CurrentPage.FilterbyComboBox.SelectedItem).Content.Equals("All"))
            {
                foreach (var material in Materials)
                {
                    ElementTemplate element = new ElementTemplate
                    {
                        Label = material.Label,
                        PriceString = material.Price + " €",
                        Quantity = material.Quantity
                    };

                    elements.Add(element);
                }
            }

            if (((ComboBoxItem)CurrentPage.FilterbyComboBox.SelectedItem).Content.Equals("Final products") || ((ComboBoxItem)CurrentPage.FilterbyComboBox.SelectedItem).Content.Equals("All"))
            {
                foreach (var product in Products)
                {
                    var listMadeOf = new List<NeededProductTemplate>();
                    foreach (var matprod in MaterialsProducts)
                    {
                        if (matprod.IdProduct == product.Id)
                        {
                            var label = Materials.First(x => x.Id == matprod.IdMaterial).Label;
                            var quantity = matprod.QuantityNeeded;
                            listMadeOf.Add(new NeededProductTemplate { Material = label, QuantityNeeded = quantity });
                        }
                    }

                    ElementTemplate element = new ElementTemplate
                    {
                        Label = product.Label,
                        PriceString = $"{product.Price:C}",
                        Quantity = product.Quantity,
                        AvailableUntilString = $"⚠ Product available until : {product.AvailableUntil:D} ⚠",
                        ColorDate = product.AvailableUntil >= DateTime.Today ? Brushes.Lime : Brushes.Red,
                        MadeOf = listMadeOf
                    };

                    elements.Add(element);
                }
            }

            return elements;
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

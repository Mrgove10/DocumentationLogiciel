using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DocumentationLogicielle.App.Templates;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ListingElementsViewModel
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

        public List<Product> Products { get; set; }
        public List<Material> Materials { get; set; }
        public List<MaterialsProduct> MaterialsProducts { get; set; }

        /// <summary>
        /// Correspond to the current page
        /// </summary>
        public ListingElementsWindow CurrentPage { get; set; }

        public UserServices UserServices { get; set; }

        public AlertServices AlertServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }
        public SaleServices SaleServices { get; set; }

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

        public void GenerateDatagrid()
        {
            CurrentPage.ListElements.ItemsSource = Convert();
        }


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
                        Price = material.Price + " €",
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
                        Price = $"{product.Price:C}",
                        Quantity = product.Quantity,
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

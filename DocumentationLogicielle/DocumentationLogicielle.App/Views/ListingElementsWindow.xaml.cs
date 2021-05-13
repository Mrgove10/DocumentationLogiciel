using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DocumentationLogicielle.App.Templates;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for ListingElementsWindow.xaml
    /// </summary>
    public partial class ListingElementsWindow : Window
    {
        public ListingElementsWindow(UserServices userServices, 
                                     AlertServices alertServices,
                                     MaterialServices materialServices,
                                     ProductServices productServices,
                                     MaterialsProductServices materialsProductServices,
                                     SaleServices saleServices,
                                     List<Product> products, 
                                     List<Material> materials, 
                                     List<MaterialsProduct> materialsProducts)
        {
            InitializeComponent();
            DataContext = new ListingElementsViewModel(this, userServices, alertServices, materialServices, productServices, materialsProductServices, saleServices, products, materials, materialsProducts);
            
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void DGrid_OnLoadingRowDetails(object? sender, DataGridRowDetailsEventArgs e)
        {
            ElementTemplate element = e.Row.Item as ElementTemplate;
            if (element.MadeOf == null)
            {
                e.Row.DetailsVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void FilterbyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).GenerateDatagrid();
            }
        }
    }
}

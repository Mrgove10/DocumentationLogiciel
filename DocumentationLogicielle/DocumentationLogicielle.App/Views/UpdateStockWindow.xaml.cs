using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for UpdateStockWindow.xaml
    /// </summary>
    public partial class UpdateStockWindow : Window
    {
        public UpdateStockWindow(UserServices userServices, 
                                     AlertServices alertServices,
                                     MaterialServices materialServices,
                                     ProductServices productServices,
                                     MaterialsProductServices materialsProductServices,
                                     SaleServices saleServices,
                                     List<Product> products,
                                     List<Material> materials)
        {
            InitializeComponent();
            DataContext = new UpdateStockViewModel(this, userServices, alertServices, materialServices, productServices, materialsProductServices, saleServices, products, materials);
        }

        private void ProductsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).GenerateForm();
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            ((DatePicker)sender).Foreground = ((DatePicker)sender).SelectedDate >= DateTime.Today ? Brushes.Lime : Brushes.Red;
        }
    }
}

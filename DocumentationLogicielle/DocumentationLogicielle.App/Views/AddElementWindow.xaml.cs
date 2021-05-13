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
    /// Interaction logic for AddElementWindow.xaml
    /// </summary>
    public partial class AddElementWindow : Window
    {
        /// <summary>
        /// This window is for adding a element 
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="alertServices"></param>
        /// <param name="materialServices"></param>
        /// <param name="productServices"></param>
        /// <param name="materialsProductServices"></param>
        /// <param name="saleServices"></param>
        /// <param name="materials"></param>
        public AddElementWindow(UserServices userServices, 
                                AlertServices alertServices, 
                                MaterialServices materialServices, 
                                ProductServices productServices, 
                                MaterialsProductServices materialsProductServices, 
                                SaleServices saleServices,
                                List<Material> materials)
        {
            InitializeComponent();
            DataContext = new AddElementViewModel(this, userServices, alertServices, materialServices, productServices, materialsProductServices, saleServices, materials);
        }
        
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TypeElementComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).GenerateForm();
            }
        }

        private void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            ((DatePicker)sender).Foreground = ((DatePicker)sender).SelectedDate >= DateTime.Today ? Brushes.Lime : Brushes.Red;
        }
    }
}

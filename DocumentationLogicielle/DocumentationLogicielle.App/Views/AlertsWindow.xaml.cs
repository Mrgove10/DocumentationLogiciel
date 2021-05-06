using System.Collections.Generic;
using System.Windows;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AlertsWindow : Window
    {
        /// <summary>
        /// Creates an alert
        /// This is also a new window that pops up
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="alertServices"></param>
        /// <param name="alerts"></param>
        public AlertsWindow(UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices, List<Alert> alerts)
        {
            InitializeComponent();
            DataContext = new AlertsViewModel(this, userServices, alertServices, materialServices, productServices, materialsProductServices, saleServices, alerts);
        }
        
    }
}

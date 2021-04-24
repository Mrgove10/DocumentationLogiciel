using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        public AlertsWindow(UserServices userServices, AlertServices alertServices, List<Alert> alerts)
        {
            InitializeComponent();
            DataContext = new AlertsViewModel(this, userServices, alertServices, alerts);
        }
        
    }
}

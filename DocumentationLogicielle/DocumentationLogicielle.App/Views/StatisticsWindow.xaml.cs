using System;
using System.Collections.Generic;
using System.Windows;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        /// <summary>
        /// Creates a new window
        /// </summary>
        public StatisticsWindow(UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices, Dictionary<string, int> countBySite, List<Tuple<string, int, int, int>> evolutionByMonth, float moneyEarned, Dictionary<int, float> moneyEarnedByYear)
        {
            InitializeComponent();
            DataContext = new StatisticsViewModel(this, userServices, alertServices, materialServices, productServices, materialsProductServices, saleServices, countBySite, evolutionByMonth, moneyEarned, moneyEarnedByYear);
        }
    }
}

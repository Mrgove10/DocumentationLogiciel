using System;
using System.Windows;
using System.Windows.Navigation;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.App.Views;

namespace DocumentationLogicielle.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NavigationService Navigation;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }
    }
}

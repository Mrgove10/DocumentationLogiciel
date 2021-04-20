using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DocumentationLogicielle.App.Services;
using DocumentationLogicielle.App.ViewModels;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(UserServices userServices)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this, userServices);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).PasswordInput = ((PasswordBox)sender).Password; }
        }
    }
}

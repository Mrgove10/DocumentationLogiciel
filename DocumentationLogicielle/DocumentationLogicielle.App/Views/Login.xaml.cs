using System.Windows;
using DocumentationLogicielle.App.ViewModels;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            DataContext = new LoginViewModel(this);
            InitializeComponent();
        }
        
        
    }
}

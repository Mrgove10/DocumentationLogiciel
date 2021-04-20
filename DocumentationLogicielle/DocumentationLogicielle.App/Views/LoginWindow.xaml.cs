using System.Windows;
using DocumentationLogicielle.App.ViewModels;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            DataContext = new LoginViewModel(this);
            InitializeComponent();
        }
        
        
    }
}

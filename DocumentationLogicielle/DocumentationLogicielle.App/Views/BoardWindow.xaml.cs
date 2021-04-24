using System.Windows;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        public BoardWindow(UserServices userServices)
        {
            InitializeComponent();
            DataContext = new BoardViewModel(this, userServices);
        }
    }
}

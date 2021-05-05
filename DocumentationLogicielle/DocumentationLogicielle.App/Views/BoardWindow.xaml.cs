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
        /// <summary>
        /// Creates a new window
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="alertServices"></param>
        /// <param name="countAlerts"></param>
        public BoardWindow(UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, int countAlerts)
        {
            InitializeComponent();
            DataContext = new BoardViewModel(this, userServices, alertServices, materialServices, productServices, materialsProductServices, countAlerts);
        }
    }
}

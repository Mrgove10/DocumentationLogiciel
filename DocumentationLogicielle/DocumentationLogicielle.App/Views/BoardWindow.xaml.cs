using System.Windows;
using DocumentationLogicielle.App.ViewModels;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        public BoardWindow()
        {
            InitializeComponent();
            DataContext = new BoardViewModel(this);
        }
    }
}

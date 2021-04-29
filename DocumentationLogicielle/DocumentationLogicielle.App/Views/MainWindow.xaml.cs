using System.Windows;
using System.Windows.Controls;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main window of the application.
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="alertServices"></param>
        public MainWindow(UserServices userServices, AlertServices alertServices)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this, userServices, alertServices);
        }

        /// <summary>
        /// Each time the user write a letter, call this function and set field in DataContext (see <see cref="MainWindowViewModel"/>)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).PasswordInput = ((PasswordBox)sender).Password; }
        }
    }
}

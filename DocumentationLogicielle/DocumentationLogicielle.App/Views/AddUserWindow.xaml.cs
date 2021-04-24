using System;
using System.Windows;
using System.Windows.Controls;
using DocumentationLogicielle.App.ViewModels;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow(UserServices userServices)
        {
            InitializeComponent();
            DataContext = new AddUserViewModel(this, userServices);
        }

        /// <summary>
        /// Each time the user write a letter, call this function and set field in DataContext (see <see cref="MainWindowViewModel"/>)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).PasswordInput = ((PasswordBox)sender).Password;
            }
        }
    }
}

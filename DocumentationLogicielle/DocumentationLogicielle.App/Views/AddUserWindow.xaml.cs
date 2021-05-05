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
        /// <summary>
        /// This window is for adding a user 
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="alertServices"></param>
        public AddUserWindow(UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices)
        {
            InitializeComponent();
            DataContext = new AddUserViewModel(this, userServices, alertServices, materialServices, productServices, materialsProductServices);
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

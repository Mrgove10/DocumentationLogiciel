using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;
using MaterialDesignThemes.Wpf;

namespace DocumentationLogicielle.App.ViewModels
{
    public class AlertsViewModel
    {
        #region Commands

        /// <summary>
        /// Command to go back to the precedent page
        /// </summary>
        public IAsyncCommand GoBackCommand { get; }

        #endregion

        /// <summary>
        /// List of alerts which are not dismiss
        /// </summary>
        public List<Alert> Alerts { get; set; }

        /// <summary>
        /// Name of the current user
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Services to interact with the table "Alert" (<see cref="Alert"/>)
        /// </summary>
        public AlertServices AlertServices { get; set; }

        /// <summary>
        /// Services to interact with the table "User" (<see cref="User"/>)
        /// </summary>
        public UserServices UserServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }
        public SaleServices SaleServices { get; set; }

        /// <summary>
        /// Correspond to the current page
        /// </summary>
        public AlertsWindow CurrentPage { get; set; }

        public AlertsViewModel(AlertsWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices, List<Alert> alerts)
        {
            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";
            CurrentPage = currentPage;
            
            AlertServices = alertServices;
            UserServices = userServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;
            
            Alerts = alerts;
            
            GenerateAlertsCards();

            GoBackCommand = new AsyncCommand(GoBack, () => true);
        }

        /// <summary>
        /// Method to go back to the precedent page
        /// Update the alerts in the same time
        /// </summary>
        /// <returns></returns>
        private async Task GoBack()
        {
            UpdateAlerts();
            BoardWindow page = new BoardWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await AlertServices.CountAlerts());
            page.Show();
            CurrentPage.Close();
        }

        /// <summary>
        /// Method to update the alert
        /// </summary>
        public void UpdateAlerts()
        {
            var childrens = CurrentPage.ListAlerts.Children;
            List<Alert> updatedAlerts = new List<Alert>();
            
            foreach (Card chidren in childrens)
            {
                Grid grid = chidren.Content as Grid;

                if ((grid?.Children[1] as Grid)?.Children[0] is CheckBox checkBox)
                {
                    var id = int.Parse(checkBox.Name.Split('_')[1]);
                    var alert = Alerts.First(x => x.Id == id);
                    if (checkBox.IsChecked != null) alert.IsDismiss = (bool)checkBox.IsChecked;

                    updatedAlerts.Add(alert);
                }
            }

            AlertServices.UpdateAlerts(updatedAlerts);
        }

        /// <summary>
        /// Generate the alerts in Cards
        /// </summary>
        public void GenerateAlertsCards()
        {
            for (int i = 0; i < Alerts.Count; i++)
            {
                CurrentPage.ListAlerts.RowDefinitions.Add(new RowDefinition());
            }

            int count = 0;
            foreach (var alert in Alerts)
            {
                Card card = new Card(); 
                StringBuilder sb = new StringBuilder();

                //Create card
                sb.Append(@"<materialDesign:Card Margin='10' Grid.Row='"+count+ @"' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:materialDesign='http://materialdesigninxaml.net/winfx/xaml/themes'>
                                <Grid>
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width='5*'></ColumnDefinition>
                                        <ColumnDefinition Width ='*'></ColumnDefinition>
                                    </Grid.ColumnDefinitions >

                                    <Grid Grid.Column='0'>
                                        <Grid.RowDefinitions>
                                            <RowDefinition Height='*'></RowDefinition>
                                            <RowDefinition Height='*'></RowDefinition>
                                            </Grid.RowDefinitions>

                                        <TextBlock Grid.Row='0'
                                                   Margin='20, 0,0,0'
                                                   VerticalAlignment='Center'
                                                   Foreground='White'
                                                   FontSize='20'
                                                   FontWeight='Bold'
                                                   Text='" + alert.Title +@"'/>

                                        <TextBlock Grid.Row = '1'
                                                   Margin = '20, 0,0,0'
                                                   VerticalAlignment = 'Center'
                                                   Foreground = 'White'
                                                   FontSize = '15'
                                                   Text = '"+alert.Message+ @"' />

                                    </Grid>
                                    <Grid Grid.Column = '1'>
                                        <CheckBox HorizontalAlignment = 'Right' IsChecked = 'False' Name='CheckBoxNumber_" + alert.Id+@"' Foreground = 'White' Content = ''>
                                            <CheckBox.LayoutTransform>
                                                <ScaleTransform ScaleX = '1.5' ScaleY = '1.5'/>
                                            </CheckBox.LayoutTransform>
                                        </CheckBox>
                                    </Grid>

                                </Grid>
                            </materialDesign:Card> ");


                card = (Card)XamlReader.Parse(sb.ToString());
                CurrentPage.ListAlerts.Children.Add(card);
                count++;
            }
        }
    }
}

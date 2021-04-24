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
        public IAsyncCommand GoBackCommand { get; }

        public List<Alert> Alerts { get; set; }

        public AlertServices AlertServices { get; set; }
        public UserServices UserServices { get; set; }

        public AlertsWindow CurrentPage { get; set; }

        public AlertsViewModel(AlertsWindow currentPage, UserServices userServices, AlertServices alertServices, List<Alert> alerts)
        {
            CurrentPage = currentPage;
            AlertServices = alertServices;
            UserServices = userServices;
            Alerts = alerts;
            
            GenerateAlertsCards();

            GoBackCommand = new AsyncCommand(GoBack, () => true);
        }

        private async Task GoBack()
        {
            UpdateAlerts();
            BoardWindow page = new BoardWindow(UserServices, AlertServices, await AlertServices.CountAlerts());
            page.Show();
            CurrentPage.Close();
        }

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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;
using LiveCharts;
using LiveCharts.Wpf;

namespace DocumentationLogicielle.App.ViewModels
{
    public class StatisticsViewModel
    {
        /// <summary>
        /// Command to go back to the precedent page
        /// </summary>
        public IAsyncCommand GoBackCommand { get; }

        #region Properties

        public SeriesCollection SeriesCollectionCountSalesBySite { get; set; }
        public SeriesCollection SeriesCollectionEvolutionByMonth { get; set; }
        public SeriesCollection SeriesCollectionMoneyByYear { get; set; }
        public string[] LabelsCountSalesBySite { get; set; }
        public string[] LabelsEvolutionByMonth { get; set; }
        public string[] LabelsMoneyByYear { get; set; }
        public Func<double, string> FormatterCountSalesBySite { get; set; }
        public Func<double, string> YFormatterEvolutionByMonth { get; set; }
        public Func<double, string> YFormatterMoneyByYear { get; set; }
        public string MoneyEarned { get; set; }

        #endregion

        /// <summary>
        /// The name of the current user
        /// </summary>
        public string CurrentUserName { get; set; }
        public StatisticsWindow CurrentPage { get; set; }

        /// <summary>
        /// Services to interact with the table "User" (<see cref="User"/>)
        /// </summary>
        public UserServices UserServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Alert" (<see cref="Alert"/>)
        /// </summary>
        public AlertServices AlertServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }
        public SaleServices SaleServices { get; set; }

        public StatisticsViewModel(StatisticsWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices, Dictionary<string, int> countBySite, List<Tuple<string, int, int, int>> evolutionByMonth, float moneyEarned, Dictionary<int, float> moneyEarnedByYear)
        {
            CurrentPage = currentPage;
            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";

            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;

            GenerateCountSalesBySite(countBySite);
            GenerateEvolutionByMonth(evolutionByMonth);
            GenerateMoneyEarnedByYear(moneyEarnedByYear);
            MoneyEarned = $"{moneyEarned:C}";

            GoBackCommand = new AsyncCommand(GoBack, () => true);
        }

        public void GenerateMoneyEarnedByYear(Dictionary<int, float> moneyEarnedByYear)
        {
            var values = new ChartValues<float>();
            foreach (var tuple in moneyEarnedByYear.OrderBy(x => x.Key))
            {
                values.Add(tuple.Value);
            }

            SeriesCollectionMoneyByYear = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Money Evolution",
                    LineSmoothness = 0,
                    Values = values
                }
            };

            LabelsMoneyByYear = new string[values.Count];
            var count = 0;
            foreach (var tuple in moneyEarnedByYear.OrderBy(x => x.Key))
            {
                LabelsMoneyByYear[count] = $"{tuple.Key}";
                count++;
            }

            YFormatterMoneyByYear = value => value.ToString("C");
        }

        public void GenerateEvolutionByMonth(List<Tuple<string, int, int, int>> evolutionByMonth)
        {
            var lineSeries = new SeriesCollection();
            foreach (var groupBySite in evolutionByMonth.GroupBy(x => x.Item1).OrderBy(x => x.Key))
            {
                var line = new LineSeries
                {
                    Title = groupBySite.Key,
                    Fill = Brushes.Transparent,
                    Values = new ChartValues<int>()
                };
                lineSeries.Add(line);
            }

            foreach (var tuple in evolutionByMonth.OrderBy(x => x.Item1).ThenBy(x => x.Item3).ThenBy(x => x.Item2))
            {
                lineSeries.First(x => x.Title == tuple.Item1).Values.Add(tuple.Item4);
            }

            SeriesCollectionEvolutionByMonth = lineSeries;

            var months = new string[12];
            int count = 0;
            foreach (var tuple in evolutionByMonth.OrderBy(x => x.Item1).ThenBy(x => x.Item3).ThenBy(x => x.Item2))
            {
                var label = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(tuple.Item2) + "-" + tuple.Item3;
                if (!months.Contains(label))
                {
                    months[count] = label;
                    count++;
                }
            }

            LabelsEvolutionByMonth = months;
            YFormatterEvolutionByMonth = value => value.ToString("####");
        }

        public void GenerateCountSalesBySite(Dictionary<string, int> countBySite)
        {
            var values = new ChartValues<double>();
            foreach (var tuple in countBySite)
            {
                values.Add(tuple.Value);
            }

            SeriesCollectionCountSalesBySite = new SeriesCollection
            {
                new ColumnSeries {Title = "Sites sale", Values = values},
            };

            LabelsCountSalesBySite = new string[values.Count];
            var count = 0;
            foreach (var tuple in countBySite)
            {
                LabelsCountSalesBySite[count] = tuple.Key;
                count++;
            }
            FormatterCountSalesBySite = value => value.ToString("####");

        }


        /// <summary>
        /// Method to go back to the precedent page
        /// </summary>
        private async Task GoBack()
        {
            BoardWindow page = new BoardWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await AlertServices.CountAlerts());
            page.Show();
            CurrentPage.Close();
        }
    }
}

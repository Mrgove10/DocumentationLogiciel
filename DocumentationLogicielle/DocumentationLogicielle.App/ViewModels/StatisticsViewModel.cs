using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;
using LiveCharts;
using LiveCharts.Wpf;

namespace DocumentationLogicielle.App.ViewModels
{
    /// <summary>
    /// View Model for the page "Statistics"
    /// </summary>
    public class StatisticsViewModel : IViewModel<StatisticsWindow, IAsyncCommand>
    {
        /// <summary>
        /// Command to go back to the precedent page
        /// </summary>
        public IAsyncCommand GoBackCommand { get; }

        #region Properties

        /// <summary>
        /// Series for the graphic "Count Sales By Site"
        /// </summary>
        public SeriesCollection SeriesCollectionCountSalesBySite { get; set; }

        /// <summary>
        /// Series for the graphic "Evolution by month"
        /// </summary>
        public SeriesCollection SeriesCollectionEvolutionByMonth { get; set; }

        /// <summary>
        /// Series for the graphic "Money by year"
        /// </summary>
        public SeriesCollection SeriesCollectionMoneyByYear { get; set; }

        /// <summary>
        /// Labels for the graphic "Count Sales By Site"
        /// </summary>
        public string[] LabelsCountSalesBySite { get; set; }

        /// <summary>
        /// Labels for the graphic "Evolution by month"
        /// </summary>
        public string[] LabelsEvolutionByMonth { get; set; }

        /// <summary>
        /// Labels for the graphic "Money by year"
        /// </summary>
        public string[] LabelsMoneyByYear { get; set; }

        /// <summary>
        /// Property to format the data in the graphic "Count Sales By Site"
        /// </summary>
        public Func<double, string> FormatterCountSalesBySite { get; set; }

        /// <summary>
        /// Property to format the data in the graphic "Evolution by month"
        /// </summary>
        public Func<double, string> YFormatterEvolutionByMonth { get; set; }

        /// <summary>
        /// Property to format the data in the graphic "Money by year"
        /// </summary>
        public Func<double, string> YFormatterMoneyByYear { get; set; }

        /// <summary>
        /// Money earned until the start
        /// </summary>
        public string MoneyEarned { get; set; }

        #endregion

        public string CurrentUserName { get; set; }
        public StatisticsWindow CurrentPage { get; set; }

        #region Services

        public UserServices UserServices { get; set; }
        public AlertServices AlertServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }
        public SaleServices SaleServices { get; set; }

        #endregion

        /// <summary>
        /// Constructor of the view model
        /// </summary>
        /// <param name="currentPage">Page of the view mode</param>
        /// <param name="userServices">Services for the "User" table</param>
        /// <param name="alertServices">Services for the "Alert" table</param>
        /// <param name="materialServices">Services for the "Material" table</param>
        /// <param name="productServices">Services for the "Product" table</param>
        /// <param name="materialsProductServices">Services for the "MaterialProduct" table</param>
        /// <param name="saleServices">Services for the "Sale" table</param>
        /// <param name="countBySite">Data to the graphic "Count Sales By Site"</param>
        /// <param name="evolutionByMonth">Data to the graphic "Evolution by month"</param>
        /// <param name="moneyEarned">Money earned until the start</param>
        /// <param name="moneyEarnedByYear">Data to the graphic "Money by year"</param>
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

        /// <summary>
        /// Generate the graphic "Money Earned By Year"
        /// </summary>
        /// <param name="moneyEarnedByYear"></param>
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

        /// <summary>
        /// Generate the graphic "Evolution by month"
        /// </summary>
        /// <param name="evolutionByMonth"></param>
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

        /// <summary>
        /// Generate the graphic "Count Sales By Site"
        /// </summary>
        /// <param name="countBySite"></param>
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

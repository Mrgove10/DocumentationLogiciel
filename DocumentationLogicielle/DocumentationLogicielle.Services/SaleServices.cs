using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// Service assigned to the sales
    /// </summary>
    public class SaleServices
    {
        /// <summary>
        /// Context of the saleservice
        /// </summary>
        private readonly SQLiteAsyncConnection _context;
        private readonly ProductServices _productServices;

        /// <summary>
        /// Sale service constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="productServices"></param>
        public SaleServices(ProjectDatabase context, ProductServices productServices)
        {
            _context = context.database;
            _productServices = productServices;
        }

        /// <summary>
        /// Count all the sales for every site
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, int>> CountBySite()
        {
            var sales = await _context.Table<Sale>().ToListAsync();

            var countBySite = new Dictionary<string, int>();
            foreach (var sale in sales)
            {
                var count = 1;
                if (countBySite.ContainsKey(sale.Site))
                {
                    count = countBySite.First(x => x.Key == sale.Site).Value;
                    count++;
                    countBySite.Remove(sale.Site);
                }

                countBySite.Add(sale.Site, count);
            }

            return countBySite;
        }

        /// <summary>
        /// Get the evolution of sales month by month
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tuple<string, int, int, int>>> EvolutionByMonth()
        {
            var sales = await _context.Table<Sale>().ToListAsync();
            
            // List<Tuple<SiteName, Month, Year, NumberOfSales>>
            List<Tuple<string, int, int, int>> evolutionByMonthAndSite = new List<Tuple<string, int, int, int>>();
            for (int i = 0; i < 12; i++)
            {
                var date = DateTime.Now.AddMonths(-i);

                foreach (ESite site in (ESite[])Enum.GetValues(typeof(ESite)))
                {
                    var salesForTheMonth = sales.Count(x => x.Site == site.ToString() && x.DateOfSale.Month == date.Month && x.DateOfSale.Year == date.Year);
                    evolutionByMonthAndSite.Add(new Tuple<string, int, int, int>(site.ToString(), date.Month, date.Year, salesForTheMonth));
                }
            }

            return evolutionByMonthAndSite;
        }

        /// <summary>
        /// Get the total amount of money earned
        /// </summary>
        /// <returns></returns>
        public async Task<float> TotalMoneyEarn()
        {
            var sales = await _context.Table<Sale>().ToListAsync();

            float total = 0;
            foreach (var sale in sales)
            {
                total += (await _productServices.GetById(sale.ProductIdSold)).Price;
            }

            return total;
        }

        /// <summary>
        /// Get all the total amount of money every year
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<int,float>> MoneyByYear()
        {
            var sales = await _context.Table<Sale>().ToListAsync();
            
            Dictionary<int, float> moneyByYear = new Dictionary<int, float>();
            for (int i = 0; i < 10; i++)
            {
                var date = DateTime.Now.AddYears(-i);
                var salesOfTheYear = sales.Where(x => x.DateOfSale.Year == date.Year).ToList();
                float total = 0;
                foreach (var sale in salesOfTheYear)
                {
                    total += (await _productServices.GetById(sale.ProductIdSold)).Price;
                }
                moneyByYear.Add(date.Year, total);
            }

            return moneyByYear;
        }
    }
}

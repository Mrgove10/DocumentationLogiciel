using System;
using System.Windows.Media;

namespace DocumentationLogicielle.App.Templates
{
    /// <summary>
    /// Template for updating the values of a product
    /// </summary>
    public class StockTemplate
    {
        /// <summary>
        /// Stock for the product
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Price for the product
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Available date for the product
        /// </summary>
        public DateTime AvailableDate { get; set; }

        /// <summary>
        /// Color for the available date of the product
        /// </summary>
        public Brush AvailableDateColor { get; set; }
    }
}

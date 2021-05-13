using System;
using System.Windows.Media;

namespace DocumentationLogicielle.App.Templates
{
    public class StockTemplate
    {
        public int IdProduct { get; set; }

        public int Stock { get; set; }

        public float Price { get; set; }

        public DateTime AvailableDate { get; set; }
        public Brush AvailableDateColor { get; set; }
    }
}

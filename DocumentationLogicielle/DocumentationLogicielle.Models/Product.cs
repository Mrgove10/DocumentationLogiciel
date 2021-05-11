using System.Collections.Generic;
using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Class for a product
    /// </summary>
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        /// <summary>
        /// Label of the product
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        public float Price { get; set; }
        
        /// <summary>
        /// Quantitiy of the product
        /// </summary>
        public int Quantity { get; set; }
    }
}

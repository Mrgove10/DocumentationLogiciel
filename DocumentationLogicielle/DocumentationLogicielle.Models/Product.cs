using System;
using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Class for the "Product" table
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id of the product
        /// This is the primary key of the table
        /// It's auto increment : 0,1,2,3 ...etc
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        /// <summary>
        /// Label of the product
        /// </summary>
        [NotNull]
        public string Label { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [NotNull]
        public float Price { get; set; }

        /// <summary>
        /// Quantity of the product
        /// </summary>
        [NotNull]
        public int Quantity { get; set; }

        /// <summary>
        /// Product available until a date
        /// </summary>
        [NotNull]
        public DateTime AvailableUntil { get; set; }
    }
}

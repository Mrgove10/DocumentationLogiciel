using System;
using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Class for a sale
    /// </summary>
    public class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Product of the Id that is Sold
        /// </summary>
        [NotNull]
        public int ProductIdSold { get; set; }

        /// <summary>
        /// Date on the sale took place
        /// </summary>
        [NotNull]
        public DateTime DateOfSale { get; set; }

        /// <summary>
        /// Site to with the sale took place
        /// </summary>
        [NotNull]
        public string Site { get; set; }
    }
}

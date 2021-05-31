using System;
using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Class for the "Sale" table
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Id of the sale
        /// This is the primary key of the table
        /// <b>It's auto increment : 0,1,2,3 ...etc</b>
        /// </summary>
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

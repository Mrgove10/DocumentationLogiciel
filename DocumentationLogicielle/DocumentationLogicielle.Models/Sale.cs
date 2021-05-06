using System;
using SQLite;

namespace DocumentationLogicielle.Models
{
    public class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int ProductIdSold { get; set; }

        [NotNull]
        public DateTime DateOfSale { get; set; }

        [NotNull]
        public string Site { get; set; }
    }
}

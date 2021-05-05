using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MaterialsProduct
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int IdMaterial { get; set; }

        public int IdProduct { get; set; }

        public int QuantityNeeded { get; set; }
    }
}

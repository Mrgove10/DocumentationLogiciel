using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Material classe for a material and product
    /// </summary>
    public class MaterialsProduct
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// id of the material
        /// </summary>
        public int IdMaterial { get; set; }

        /// <summary>
        /// Idof the product
        /// </summary>
        public int IdProduct { get; set; }

        /// <summary>
        /// Quantity nedded for this material
        /// </summary>
        public int QuantityNeeded { get; set; }
    }
}

using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Class for the "MaterialProduct" table
    /// </summary>
    public class MaterialsProduct
    {
        /// <summary>
        /// Id of the user
        /// This is the primary key of the table
        /// It's auto increment : 0,1,2,3 ...etc
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Id of the material
        /// </summary>
        public int IdMaterial { get; set; }

        /// <summary>
        /// Id of the product
        /// </summary>
        public int IdProduct { get; set; }

        /// <summary>
        /// Quantity needed for this material
        /// </summary>
        public int QuantityNeeded { get; set; }
    }
}

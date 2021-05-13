using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Material class
    /// </summary>
    public class Material
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Label assigned to the material
        /// </summary>
        [NotNull]
        public string Label { get; set; }

        /// <summary>
        /// Unitary price 
        /// </summary>
        [NotNull]
        public float Price { get; set; }

        /// <summary>
        /// Quantity available  
        /// </summary>
        [NotNull]
        public int Quantity { get; set; }
    }
}

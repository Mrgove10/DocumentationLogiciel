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
        public string Label { get; set; }

        /// <summary>
        /// Unitary price 
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Quantity available  
        /// </summary>
        public int Quantity { get; set; }
    }
}

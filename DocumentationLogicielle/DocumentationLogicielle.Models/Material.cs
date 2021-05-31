using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Class for the "Material" table
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Id of the material
        /// This is the primary key of the table
        /// It's auto increment : 0,1,2,3 ...etc
        /// </summary>
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

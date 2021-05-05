using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Material
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Label { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }
    }
}

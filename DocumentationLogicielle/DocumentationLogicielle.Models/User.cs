using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Class for the "User" table
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of the user
        /// This is the primary key of the table
        /// It's auto increment : 0,1,2,3 ...etc
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Username of the user
        /// Primary key of the table
        /// Can't be null
        /// </summary>
        [NotNull]
        public string Login { get; set; }

        /// <summary>
        /// Password of the user
        /// Can't be null
        /// </summary>
        [NotNull]
        public string Password { get; set; }

        /// <summary>
        /// Role of the user
        /// Can't be null
        /// </summary>
        [NotNull] 
        public string Role { get; set; }
    }
}

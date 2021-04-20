using SQLite;

namespace DocumentationLogicielle.App.Models
{
    public class User
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Ceci est un login
        /// </summary>
        [NotNull]
        public string Login { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [NotNull]
        public string Password { get; set; }

        /// <summary>
        /// Role of the user
        /// </summary>
        [NotNull] 
        public string Role { get; set; } = ERole.User.ToString();
    }
}

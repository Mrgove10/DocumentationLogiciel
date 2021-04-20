using SQLite;

namespace DocumentationLogicielle.App.Models
{
    public class User
    {
        /// <summary>
        /// Id de l'utilisateur
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Ceci est un login
        /// </summary>
        [NotNull]
        public string Login { get; set; }

        /// <summary>
        /// Password de l'utilisateur
        /// </summary>
        [NotNull]
        public string Password { get; set; }
    }
}

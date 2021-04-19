using System;

namespace DocumentationLogicielle.Models
{
    public class User
    {
        /// <summary>
        /// Ceci est le login de l'utilisateur
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Voici son mot de passe
        /// </summary>
        public string Password { get; set; }
    }
}

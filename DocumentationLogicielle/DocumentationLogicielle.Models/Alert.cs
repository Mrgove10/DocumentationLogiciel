using SQLite;

namespace DocumentationLogicielle.Models
{
    public class Alert
    {
        /// <summary>
        /// Id of the alert
        /// This is the primary key of the table
        /// It's auto increment : 0,1,2,3 ...etc
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Title of the alert
        /// Can't be null
        /// </summary>
        [NotNull]
        public string Title { get; set; }

        /// <summary>
        /// Message of the alert
        /// Can't be null
        /// </summary>
        [NotNull]
        public string Message { get; set; }

        /// <summary>
        /// Boolean indicates if the alert is dismiss
        /// Can't be null
        /// Default is False
        /// </summary>
        [NotNull]
        public bool IsDismiss { get; set; } = false;
        
        /// <summary>
        /// Id of the materiel
        /// Can't be null
        /// </summary>
        [NotNull]
        public int MaterialId { get; set; }
    }
}

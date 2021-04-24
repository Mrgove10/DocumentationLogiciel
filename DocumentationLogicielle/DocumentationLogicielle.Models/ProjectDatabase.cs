using SQLite;

namespace DocumentationLogicielle.Models
{
    public class ProjectDatabase
    {
        public readonly SQLiteAsyncConnection database;

        public ProjectDatabase()
        {
            database = new SQLiteAsyncConnection(@"E:\Cours\I1\Documentation Logicielle\Projet groupe\DocumentationLogicielle.db3");
            database.CreateTableAsync<User>().Wait();
            database.CreateTableAsync<Alert>().Wait();
            //database.InsertAsync(new User { Login = "user", Password = "user", Role = ERole.User.ToString() });
            //database.InsertAsync(new User { Login = "admin", Password = "admin", Role = ERole.Administrator.ToString() });
            //database.InsertAsync(new User { Login = "lou", Password = "lou", Role = ERole.Administrator.ToString() });
            //database.InsertAsync(new Alert { Title = "Stock critique de colles", Message = "Le stock de colles est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false});
            //database.InsertAsync(new Alert { Title = "Stock critique de vis", Message = "Le stock de vis est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false});
            //database.InsertAsync(new Alert { Title = "Stock critique de peintures", Message = "Le stock de peintures est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false});
            //database.InsertAsync(new Alert { Title = "Stock critique de bois", Message = "Le stock de bois est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = true});
        }
    }
}

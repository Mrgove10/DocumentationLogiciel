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
            //database.InsertAsync(new User { Login = "user", Password = "user", Role = ERole.User.ToString() });
            //database.InsertAsync(new User { Login = "admin", Password = "admin", Role = ERole.Administrator.ToString() });
        }
    }
}

using System;
using System.IO;
using SQLite;

namespace DocumentationLogicielle.Models
{
    public class ProjectDatabase
    {
        public readonly SQLiteAsyncConnection database;

        public ProjectDatabase()
        {
            database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DocumentationLogicielle.db3"));
            database.CreateTableAsync<User>().Wait();
        }
    }
}

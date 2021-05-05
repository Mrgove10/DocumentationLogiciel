using SQLite;

namespace DocumentationLogicielle.Models
{
    /// <summary>
    /// Main Database class of the project
    /// This class contains the main reference to the database and can be reused
    /// </summary>
    public class ProjectDatabase
    {
        public readonly SQLiteAsyncConnection database;

        public ProjectDatabase()
        {
            database = new SQLiteAsyncConnection(@"E:\Cours\I1\Documentation Logicielle\Projet groupe\DocumentationLogicielle.db3");
            database.CreateTableAsync<User>().Wait();
            database.CreateTableAsync<Alert>().Wait();
            database.CreateTableAsync<Product>().Wait();
            database.CreateTableAsync<Material>().Wait();
            database.CreateTableAsync<MaterialsProduct>().Wait();
            //database.InsertAsync(new User { Login = "user", Password = "user", Role = ERole.User.ToString() });
            //database.InsertAsync(new User { Login = "admin", Password = "admin", Role = ERole.Administrator.ToString() });
            //database.InsertAsync(new User { Login = "lou", Password = "lou", Role = ERole.Administrator.ToString() });
            //database.InsertAsync(new Alert { Title = "Stock critique de colles", Message = "Le stock de colles est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false});
            //database.InsertAsync(new Alert { Title = "Stock critique de vis", Message = "Le stock de vis est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false});
            //database.InsertAsync(new Alert { Title = "Stock critique de peintures", Message = "Le stock de peintures est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false});
            //database.InsertAsync(new Alert { Title = "Stock critique de bois", Message = "Le stock de bois est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = true});
            //database.InsertAllAsync(new List<Material>
            //{
            //    new() {Label = "Wood", Price = 1.5f, Quantity = 15},
            //    new() {Label = "Stone", Price = 2.7f, Quantity = 32},
            //    new() {Label = "Screw", Price = 0.4f, Quantity = 41},
            //    new() {Label = "Glue", Price = 5.6f, Quantity = 07}
            //});
            //database.InsertAllAsync(new List<Product>
            //{
            //    new (){Label = "House", Price = 250400, Quantity = 1},
            //    new (){Label = "Roof", Price = 16321, Quantity = 3}
            //});
            //database.InsertAllAsync(new List<MaterialsProduct>
            //{
            //    new() {IdProduct = 1, IdMaterial = 1, QuantityNeeded = 152},
            //    new() {IdProduct = 1, IdMaterial = 2, QuantityNeeded = 126},
            //    new() {IdProduct = 1, IdMaterial = 3, QuantityNeeded = 418},
            //    new() {IdProduct = 2, IdMaterial = 1, QuantityNeeded = 76},
            //    new() {IdProduct = 2, IdMaterial = 4, QuantityNeeded = 23}
            //});
        }
    }
}

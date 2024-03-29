﻿using System;
using System.Collections.Generic;
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
            database = new SQLiteAsyncConnection(@"DocumentationLogicielle.db3");
            //database = new SQLiteAsyncConnection(@"E:\Cours\I1\Documentation Logicielle\Projet groupe\DocumentationLogicielle.db3");
            database.CreateTableAsync<User>().Wait();
            database.CreateTableAsync<Alert>().Wait();
            database.CreateTableAsync<Product>().Wait();
            database.CreateTableAsync<Material>().Wait();
            database.CreateTableAsync<MaterialsProduct>().Wait();
            database.CreateTableAsync<Sale>().Wait();

            //database.InsertAsync(new User { Login = "user", Password = BCrypt.Net.BCrypt.HashPassword("user"), Role = ERole.User.ToString() });
            //database.InsertAsync(new User { Login = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Role = ERole.Administrator.ToString() });
            //database.InsertAllAsync(new List<Material>
            //{
            //    new() {Label = "Wood", Price = 1.5f, Quantity = 15},
            //    new() {Label = "Stone", Price = 2.7f, Quantity = 132},
            //    new() {Label = "Screw", Price = 0.4f, Quantity = 41},
            //    new() {Label = "Glue", Price = 3.6f, Quantity = 7},
            //    new() {Label = "Painting", Price = 5.2f, Quantity = 22}
            //});
            //database.InsertAllAsync(new List<Product>
            //{
            //    new (){Label = "House", Price = 250400, Quantity = 1, AvailableUntil = new DateTime(2021,08,07)},
            //    new (){Label = "Roof", Price = 16321, Quantity = 3, AvailableUntil = new DateTime(2021,06,13)},
            //    new (){Label = "Terrace", Price = 3412, Quantity = 2, AvailableUntil = new DateTime(2021,05,13)},
            //    new (){Label = "Shelter", Price = 1486, Quantity = 6, AvailableUntil = new DateTime(2020,11,04)},
            //});
            //database.InsertAllAsync(new List<MaterialsProduct>
            //{
            //    new() {IdProduct = 1, IdMaterial = 1, QuantityNeeded = 152},
            //    new() {IdProduct = 1, IdMaterial = 2, QuantityNeeded = 126},
            //    new() {IdProduct = 1, IdMaterial = 3, QuantityNeeded = 418},
            //    new() {IdProduct = 2, IdMaterial = 1, QuantityNeeded = 76},
            //    new() {IdProduct = 2, IdMaterial = 4, QuantityNeeded = 23},
            //    new() {IdProduct = 4, IdMaterial = 1, QuantityNeeded = 514},
            //    new() {IdProduct = 3, IdMaterial = 2, QuantityNeeded = 421},
            //});
            //database.InsertAsync(new Alert { Title = "Stock critique de colles", Message = "Le stock de colles est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false, MaterialId = 4 });
            //database.InsertAsync(new Alert { Title = "Stock critique de vis", Message = "Le stock de vis est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false, MaterialId = 3 });
            //database.InsertAsync(new Alert { Title = "Stock critique de peintures", Message = "Le stock de peintures est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = false, MaterialId = 5 });
            //database.InsertAsync(new Alert { Title = "Stock critique de bois", Message = "Le stock de bois est au plus bas. Contactez le fournisseur pour recommander du stock.", IsDismiss = true, MaterialId = 1 });

            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now, Site = ESite.Annecy.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 2, DateOfSale = DateTime.Now, Site = ESite.Annecy.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddDays(-2), Site = ESite.Annecy.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddDays(-1), Site = ESite.Lyon.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddDays(-7), Site = ESite.Lyon.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddDays(-3), Site = ESite.Lyon.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddDays(-3), Site = ESite.Lyon.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddDays(-4), Site = ESite.Annecy.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddMonths(-2).AddDays(-5), Site = ESite.Annecy.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddMonths(-2).AddDays(-3), Site = ESite.Annecy.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddMonths(-2).AddDays(-7), Site = ESite.Paris.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddMonths(-2).AddDays(-4), Site = ESite.Toulouse.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddDays(-5), Site = ESite.Toulouse.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddYears(-1).AddDays(-10), Site = ESite.Toulouse.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddYears(-1).AddMonths(-1).AddDays(-10), Site = ESite.Toulouse.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddYears(-1).AddDays(-12), Site = ESite.Annecy.ToString() });
            //database.InsertAsync(new Sale { ProductIdSold = 1, DateOfSale = DateTime.Now.AddYears(-1).AddDays(-15), Site = ESite.Annecy.ToString() });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// Services for the "Product" table
    /// </summary>
    public class ProductServices
    {
        private readonly SQLiteAsyncConnection _context;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context of the database</param>
        public ProductServices(ProjectDatabase context)
        {
            _context = context.database;
        }

        /// <summary>
        /// Get all the products which are still available
        /// </summary>
        /// <returns>List of Products</returns>
        public async Task<List<Product>> GetAll()
        {
            return await _context.Table<Product>().ToListAsync();
        }

        /// <summary>
        /// Get a product for a certain id
        /// </summary>
        /// <param name="idProduct">id of the product</param>
        /// <returns>Product</returns>
        public async Task<Product> GetById(int idProduct)
        {
            return await _context.Table<Product>().FirstAsync(x => x.Id == idProduct);
        }

        /// <summary>
        /// Get a product with its label
        /// </summary>
        /// <param name="labelProduct">The label of the product</param>
        /// <returns>The product</returns>
        public async Task<Product> GetByLabel(string labelProduct)
        {
            return await _context.Table<Product>().FirstOrDefaultAsync(x => x.Label == labelProduct);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="productToUpdate">The product to update</param>
        public void Update(Product productToUpdate)
        {
            _context.UpdateAsync(productToUpdate).Wait();
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="product">The product to create</param>
        public void Create(Product product)
        {
            _context.InsertAsync(product).Wait();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">The product to delete</param>
        public void Delete(Product product)
        {
            _context.DeleteAsync(product).Wait();
        }
    }
}

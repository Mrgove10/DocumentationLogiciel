using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// Product service
    /// </summary>
    public class ProductServices
    {
        private readonly SQLiteAsyncConnection _context;

        /// <summary>
        /// Service of the product (database)
        /// </summary>
        /// <param name="context"></param>
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

        public async Task<Product> GetByLabel(string labelProduct)
        {
            return await _context.Table<Product>().FirstOrDefaultAsync(x => x.Label == labelProduct);
        }

        public void Update(Product productToUpdate)
        {
            _context.UpdateAsync(productToUpdate).Wait();
        }

        public void Create(Product product)
        {
            _context.InsertAsync(product).Wait();
        }

        public void Delete(Product product)
        {
            _context.DeleteAsync(product).Wait();
        }
    }
}

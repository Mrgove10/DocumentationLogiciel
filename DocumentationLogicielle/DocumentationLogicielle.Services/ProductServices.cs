using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ProductServices
    {
        private readonly SQLiteAsyncConnection _context;

        public ProductServices(ProjectDatabase context)
        {
            _context = context.database;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Table<Product>().ToListAsync();
        }
    }
}

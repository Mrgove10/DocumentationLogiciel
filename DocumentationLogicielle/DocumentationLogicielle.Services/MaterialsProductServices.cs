using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MaterialsProductServices
    {
        private readonly SQLiteAsyncConnection _context;

        public MaterialsProductServices(ProjectDatabase context)
        {
            _context = context.database;
        }

        public async Task<List<MaterialsProduct>> GetAll()
        {
            return await _context.Table<MaterialsProduct>().ToListAsync();
        }
    }
}

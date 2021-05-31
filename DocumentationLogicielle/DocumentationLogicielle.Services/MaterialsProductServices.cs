using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// Services for the "MaterialsProduct" table
    /// </summary>
    public class MaterialsProductServices
    {
        private readonly SQLiteAsyncConnection _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Database context</param>
        public MaterialsProductServices(ProjectDatabase context)
        {
            _context = context.database;
        }
        
        /// <summary>
        /// Get all the MaterialsProduct as a list
        /// </summary>
        /// <returns>List of MaterialsProduct</returns>
        public async Task<List<MaterialsProduct>> GetAll()
        {
            return await _context.Table<MaterialsProduct>().ToListAsync();
        }

        /// <summary>
        /// Add a list of materials for a product
        /// </summary>
        /// <param name="materialsProducts"></param>
        public void AddNeededMaterials(List<MaterialsProduct> materialsProducts)
        {
            _context.InsertAllAsync(materialsProducts).Wait();
        }
    }
}

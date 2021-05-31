using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// Services for the "Material" table
    /// </summary>
    public class MaterialServices
    {
        private readonly SQLiteAsyncConnection _context;
        private readonly AlertServices _alertServices;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context of the database</param>
        /// <param name="alertServices">Services for the "Alert" table</param>
        public MaterialServices(ProjectDatabase context, AlertServices alertServices)
        {
            _context = context.database;
            _alertServices = alertServices;
        }

        /// <summary>
        /// Get all the materials
        /// </summary>
        /// <returns></returns>
        public async Task<List<Material>> GetAll()
        {
            return await _context.Table<Material>().ToListAsync();
        }

        /// <summary>
        /// Get a material by its label
        /// </summary>
        /// <param name="labelMaterial">Label of the material</param>
        /// <returns>The material</returns>
        public async Task<Material> GetByLabel(string labelMaterial)
        {
            return await _context.Table<Material>().FirstOrDefaultAsync(x => x.Label == labelMaterial);
        }

        /// <summary>
        /// Update a material
        /// </summary>
        /// <param name="materialToUpdate">Material to update</param>
        public void Update(Material materialToUpdate)
        {
            _context.UpdateAsync(materialToUpdate).Wait();
        }

        /// <summary>
        /// Create a material
        /// </summary>
        /// <param name="material">Material to create</param>
        public void Create(Material material)
        {
            _context.InsertAsync(material).Wait();
        }

        /// <summary>
        /// Delete a material
        /// </summary>
        /// <param name="material">Material to delete</param>
        /// <returns></returns>
        public async Task Delete(Material material)
        {
            await _alertServices.DeleteAlertOfMaterial(material.Id);
            _context.DeleteAsync(material).Wait();
        }
    }
}

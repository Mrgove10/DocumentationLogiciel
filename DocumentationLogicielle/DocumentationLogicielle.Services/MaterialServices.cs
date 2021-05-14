using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MaterialServices
    {
        private readonly SQLiteAsyncConnection _context;
        private readonly AlertServices _alertServices;

        public MaterialServices(ProjectDatabase context, AlertServices alertServices)
        {
            _context = context.database;
            _alertServices = alertServices;
        }

        public async Task<List<Material>> GetAll()
        {
            return await _context.Table<Material>().ToListAsync();
        }

        public async Task<Material> GetByLabel(string labelMaterial)
        {
            return await _context.Table<Material>().FirstOrDefaultAsync(x => x.Label == labelMaterial);
        }

        public void Update(Material materialToUpdate)
        {
            _context.UpdateAsync(materialToUpdate).Wait();
        }

        public void Create(Material material)
        {
            _context.InsertAsync(material).Wait();
        }

        public async Task Delete(Material material)
        {
            await _alertServices.DeleteAlertOfMaterial(material.Id);
            _context.DeleteAsync(material).Wait();
        }
    }
}

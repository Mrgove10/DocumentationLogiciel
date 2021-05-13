using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    public class AlertServices
    {
        private readonly SQLiteAsyncConnection _context;

        public AlertServices(ProjectDatabase context)
        {
            _context = context.database;
        }

        /// <summary>
        /// Count the number of alerts not dismissed
        /// </summary>
        /// <returns>Return an int</returns>
        public async Task<int> CountAlerts()
        {
            return await _context.Table<Alert>().CountAsync(x => !x.IsDismiss);
        }

        /// <summary>
        /// Get all alerts which are not dismissed
        /// </summary>
        /// <returns>Return a list of alerts</returns>
        public async Task<List<Alert>> GetAllAlerts()
        {
            return await _context.Table<Alert>().Where(x => !x.IsDismiss).ToListAsync();
        }

        public async Task<Alert> GetAlertByMaterial(int materialId)
        {
            return await _context.Table<Alert>().FirstOrDefaultAsync(x => x.MaterialId == materialId);
        }

        public void UpdateAlert(Alert alert)
        {
            _context.UpdateAsync(alert).Wait();
        }

        /// <summary>
        /// Method to update a list of alerts
        /// </summary>
        /// <param name="alerts">List of alerts to update</param>
        public void UpdateAlerts(List<Alert> alerts)
        {
            _context.UpdateAllAsync(alerts).Wait();
        }

        public void Create(Alert alert)
        {
            _context.InsertAsync(alert).Wait();
        }
    }
}

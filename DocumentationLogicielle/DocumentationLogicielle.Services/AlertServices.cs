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

        public async Task<int> CountAlerts()
        {
            return await _context.Table<Alert>().CountAsync(x => !x.IsDismiss);
        }

        public async Task<List<Alert>> GetAllAlerts()
        {
            return await _context.Table<Alert>().Where(x => !x.IsDismiss).ToListAsync();
        }

        public void UpdateAlerts(List<Alert> alerts)
        {
            _context.UpdateAllAsync(alerts).Wait();
        }
    }
}

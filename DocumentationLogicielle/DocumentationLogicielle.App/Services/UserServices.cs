using System.Threading.Tasks;
using DocumentationLogicielle.App.Models;
using SQLite;

namespace DocumentationLogicielle.App.Services
{
    public class UserServices
    {
        private readonly SQLiteAsyncConnection _context;

        public UserServices(ProjectDatabase context)
        {
            _context = context.database;
        }

        public async Task<bool> IsUserExists(string login, string password)
        {
            
            return await _context.Table<User>().CountAsync(x => x.Login == login && x.Password == password) > 0;
        }

        public async Task<User> GetUser(string login, string password)
        {
            return await _context.Table<User>().FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }
    }
}

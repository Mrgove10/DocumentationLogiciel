using System.Threading.Tasks;
using DocumentationLogicielle.Models;
using SQLite;

namespace DocumentationLogicielle.Services
{
    /// <summary>
    /// Services for the "User" table
    /// </summary>
    public class UserServices
    {
        private readonly SQLiteAsyncConnection _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context of the database</param>
        public UserServices(ProjectDatabase context)
        {
            _context = context.database;
        }

        /// <summary>
        /// Check if a user exists in the database
        /// <remarks>This function is mainly used for the login page</remarks>
        /// see <see cref="User"/>
        /// </summary>
        /// <param name="login">Login of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>Boolean : <b>True</b>, the user exists or <b>False</b>, the user does not exist</returns>
        public async Task<bool> IsUserExists(string login, string password)
        {
            return await _context.Table<User>().CountAsync(x => x.Login == login) > 0;
        }

        /// <summary>
        /// Get a user
        /// see <see cref="User"/>
        /// </summary>
        /// <param name="login">Login of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>If the user exists <b>and the password is correct</b> : <b>all his information</b> OR If the user does not exist : <b>Null</b></returns>
        public async Task<User> GetUser(string login, string password)
        {
            var tempUser = await _context.Table<User>().FirstOrDefaultAsync(x => x.Login == login);
            // check that the password is correct 
            if (BCrypt.Net.BCrypt.Verify(password,tempUser.Password))
            {
                return tempUser;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="login">Login of the user</param>
        /// <param name="password">Password of the user</param>
        /// <param name="role">Role of the user</param>
        public void CreateUser(string login, string password, string role)
        {
            _context.InsertAsync(new User
            {
                Login = login,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role
            }).Wait();
        }
    }
}
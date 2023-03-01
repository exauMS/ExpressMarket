using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressMarket.Services
{
    internal class UserService
    {
        private static SQLiteAsyncConnection db;
        
        private static async Task Init()
        {
            if (db != null)
                return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ExpressMarket.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<User>();
        }

        public static async Task AddUserAsync(User user)
        {
            await Init();
            await db.InsertAsync(user);
        }

        public static async Task DeleteUserByIdAsync(int id)
        {
            await Init();
            await db.DeleteAsync<User>(id);
        }

        public static async Task<IEnumerable<User>>GetUsers()
        {
            await Init();
            var users = await db.Table<User>().ToListAsync();
            return users;
        }
        public static async Task<User> GetUserByUserNameAsync(string userName)
        {
            await Init();
            var user = from u in db.Table<User>()
                       where u.UserName == userName
                       select u;
            return await user.FirstOrDefaultAsync();
        }
    }
}

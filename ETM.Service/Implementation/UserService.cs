using ETM.Data;
using ETM.Entity;
using ETM.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateUser(User user)
        {
            await _db.Users.AddAsync(user);
            return await Save();
        }

        public async Task<bool> DeleteUser(User user)
        {
            _db.Remove(user);
            return await Save();
        }

        public User GetUser(int userId)
        {
            return _db.Users.FirstOrDefault(x => x.Id == userId);
        }

        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(x => x.Id).ToList();
        }

        private async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> UpdateUser(User user)
        {
             _db.Users.Update(user);
            return await Save();
        }

        public bool UserExists(string username)
        {
            return _db.Users.Any(x => x.Username.ToLower().Trim() == username.ToLower().Trim());
        }

        public bool UserExists(int userId)
        {
            return _db.Users.Any(x => x.Id == userId);
        }
    }
}

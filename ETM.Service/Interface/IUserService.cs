using ETM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Interface
{
    public interface IUserService
{
        ICollection<User> GetUsers();
        User GetUser(int userId);
        bool UserExists(string username);
        bool UserExists(int userId);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
    }
}

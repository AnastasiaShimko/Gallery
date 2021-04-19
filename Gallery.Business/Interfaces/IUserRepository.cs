using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CheckUser(User user);
        Task<User> GetUserByEmail(User user);
        Task CreateUserAsync(User user);
    }
}

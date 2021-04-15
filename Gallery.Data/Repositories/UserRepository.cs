using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GalleryDbContext db;

        public UserRepository(GalleryDbContext context)
        {
            db = context;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            db.Users.Add(new User { Email = user.Email, Password = user.Password });
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public Task<User> CheckUser(User user)
        {
            return db.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
        }

        public Task<User> GetUserByEmail(User user)
        {
            return db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        }
    }
}

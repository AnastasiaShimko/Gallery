using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GalleryDbContext db;

        public CategoryRepository(GalleryDbContext context)
        {
            db = context;
        }

        public async Task CreateCategory(Category category)
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryid)
        {
            Category category = db.Categories.FirstOrDefault(c => c.ID == categoryid);
            if (category != null)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
        }

        public Task<List<Category>> GetAllCategories()
        {
            return db.Categories.ToListAsync();
        }

        public Category GetCategoryById(int id)
        {
            return db.Categories.FirstOrDefault(c => c.ID == id);
        }

        public async Task UpdateCategory(Category category)
        {
            Category dbCategory = await db.Categories.FirstOrDefaultAsync(c => c.ID == category.ID);
            if (dbCategory != null)
            {
                dbCategory.Name = category.Name;
                dbCategory.Description = category.Description;
                await db.SaveChangesAsync();
            }
        }
    }
}

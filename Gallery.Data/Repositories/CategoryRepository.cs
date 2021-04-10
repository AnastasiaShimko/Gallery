using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Gallery.Models;

namespace Gallery.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GalleryDbContext db;

        public CategoryRepository(GalleryDbContext context)
        {
            db = context;
        }

        public bool CreateCategory(Category category)
        {
            var dbCategpry = new Category()
            {
                Name = category.Name,
                Description = category.Description
            };
            db.Categories.Add(dbCategpry);
            var result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteCategory(int categoryid)
        {
            Category category = db.Categories.FirstOrDefault(c => c.ID == categoryid);
            if (category != null)
            {
                db.Categories.Remove(category);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Category> GetAllCategories()
        {
            var result =  db.Categories.ToList();

            return result;
        }

        public Category GetCategoryById(int id)
        {
            return db.Categories.FirstOrDefault(c => c.ID == id);
        }

        public bool UpdateCategory(Category category)
        {
            Category dbCategory = db.Categories.FirstOrDefault(c => c.ID == category.ID);
            if (dbCategory != null)
            {
                dbCategory.Name = category.Name;
                dbCategory.Description = category.Description;
                var result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

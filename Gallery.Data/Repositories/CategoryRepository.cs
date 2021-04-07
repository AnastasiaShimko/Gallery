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

        public bool CreateCategory(string name, string description)
        {
            var categpry = new Category()
            {
                Name = name,
                Description = description
            };
            db.Categories.Add(categpry);
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

        public bool UpdateCategory(int categoryid, string name, string description)
        {
            Category category = db.Categories.FirstOrDefault(c => c.ID == categoryid);
            if (category != null)
            {
                category.Name = name;
                category.Description = description;
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

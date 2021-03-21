using System;
using System.Collections.Generic;
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

        public List<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }
    }
}

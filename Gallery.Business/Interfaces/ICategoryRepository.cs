using System;
using System.Collections.Generic;
using System.Text;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int categoryid);
    }
}

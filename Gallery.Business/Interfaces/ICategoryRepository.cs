using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Category GetCategoryById(int id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryid);
    }
}

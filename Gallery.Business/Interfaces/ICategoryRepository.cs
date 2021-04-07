using System;
using System.Collections.Generic;
using System.Text;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        bool CreateCategory(string name, string description);
        bool UpdateCategory(int categoryid, string name, string description);
        bool DeleteCategory(int categoryid);
    }
}

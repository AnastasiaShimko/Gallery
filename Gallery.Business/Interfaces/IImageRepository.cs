using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface IImageRepository
    {
        Task CreateImage(Image image);
        Task<Image> GetImageById(int id);
        Task UpdateImage(Image image);
        Task DeleteImage(int imageid);
        Task<List<Image>> GetLastFiveImagesByCategory(int categoryid);
        Task<List<Image>> GetAllImagesByCategory(int categoryid);
        Task<List<Image>> SearchImagesByString(string searchString);
        Task<List<Image>> GetAllImages();
    }
}

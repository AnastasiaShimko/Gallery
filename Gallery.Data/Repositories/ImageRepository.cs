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
    public class ImageRepository : IImageRepository
    {
        private readonly GalleryDbContext db;

        public ImageRepository(GalleryDbContext context)
        {
            db = context;
        }

        public Task<List<Image>> GetLastFiveImagesByCategory(int categoryid)
        {
            var imagesList = db.Images.OrderByDescending(s => s.ID).Where(images => images.Categories.Any(c => c.ID == categoryid)).ToList();
            return Task.FromResult((imagesList.Count < 5) switch
            {
                true => imagesList,
                _ => imagesList.GetRange(0, 5)
            });
        }

        public async Task CreateImage(Image image)
        {
            await db.Images.AddAsync(image);
            await db.SaveChangesAsync();
        }

        public async Task UpdateImage(Image image)
        {
            Image dbImage = db.Images.FirstOrDefault(c => c.ID == image.ID);
            if (dbImage != null)
            {
                dbImage.Categories = image.Categories;
                dbImage.Author = image.Author;
                dbImage.Data = image.Data;
                dbImage.Format = image.Format;
                dbImage.Title = image.Title;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteImage(int imageid)
        {
            Image image = db.Images.FirstOrDefault(c => c.ID == imageid);
            if (image != null)
            {
                db.Images.Remove(image);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Image> GetImageById(int id)
        {
            return await db.Images.FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<List<Image>> GetAllImagesByCategory(int categoryid)
        {
            return await db.Images.Where(images => images.Categories.Any(c => c.ID == categoryid)).ToListAsync();
        }

        public async Task<List<Image>> SearchImagesByString(string searchString)
        {
            return await db.Images.Where(s => s.Title.Contains(searchString)).ToListAsync();
        }
    }
}

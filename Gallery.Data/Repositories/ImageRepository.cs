using System;
using System.Collections.Generic;
using System.Text;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Gallery.Models;

namespace Gallery.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly GalleryDbContext db;

        public ImageRepository(GalleryDbContext context)
        {
            db = context;
        }

        public bool CreateImage(string title, string author, string format, byte[] data, List<Category> categories)
        {
            var image = new Image()
            {
                Author = author,
                Categories =  categories,
                Format = format,
                Title = title,
                Data = data
            };
            db.Images.Add(image);
            var result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}

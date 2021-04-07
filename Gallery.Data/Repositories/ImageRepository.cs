using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Image> GetLastFiveImagesByCategory(int categoryid)
        {
            var result = new List<Image>();

            result  = db.Images.Where(images => images.Categories.Any(c => c.ID == categoryid)).ToList();

            return result;
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

        public bool UpdateImage(int imageid, string title, string author, string format, byte[] data, List<Category> categories)
        {
            Image image = db.Images.FirstOrDefault(c => c.ID == imageid);
            if (image != null)
            {
                image.Categories = categories;
                image.Author = author;
                image.Data = data;
                image.Format = format;
                image.Title = title;
                var result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool DeleteImage(int imageid)
        {
            Image image = db.Images.FirstOrDefault(c => c.ID == imageid);
            if (image != null)
            {
                db.Images.Remove(image);
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

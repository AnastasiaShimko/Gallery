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
            var images = db.Images.OrderByDescending(s => s.ID).Where(images => images.Categories.Any(c => c.ID == categoryid)).ToList();
            if (images.Count < 5)
            {
                return images;
            }
            else
            {
                return images.GetRange(0, 5);
            }
        }

        public bool CreateImage(Image image)
        {
            db.Images.Add(image);
            var result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateImage(Image image)
        {
            Image dbImage = db.Images.FirstOrDefault(c => c.ID == image.ID);
            if (dbImage != null)
            {
                dbImage.Categories = image.Categories;
                dbImage.Author = image.Author;
                dbImage.Data = image.Data;
                dbImage.Format = image.Format;
                dbImage.Title = image.Title;
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

        public Image GetImageById(int id)
        {
            return db.Images.FirstOrDefault(c => c.ID == id);
        }

        public List<Image> GetAllImagesByCategory(int categoryid)
        {
            return db.Images.Where(images => images.Categories.Any(c => c.ID == categoryid)).ToList();
        }
    }
}

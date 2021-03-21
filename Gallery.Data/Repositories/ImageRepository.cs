using System;
using System.Collections.Generic;
using System.Text;
using Gallery.Business.Interfaces;
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

    }
}

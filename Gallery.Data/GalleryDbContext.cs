using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Gallery.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Models
{
    public class GalleryDbContext : DbContext
    {
        public GalleryDbContext(DbContextOptions<GalleryDbContext> options) : base(options)
        {

        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

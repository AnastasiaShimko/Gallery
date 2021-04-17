using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface IImageRepository
    {
        public bool CreateImage(Image image);
        public Image GetImageById(int id);
        public bool UpdateImage(Image image);
        public bool DeleteImage(int imageid);
        public List<Image> GetLastFiveImagesByCategory(int categoryid);
        public List<Image> GetAllImagesByCategory(int categoryid);
        public List<Image> SearchImagesByString(string searchString);
    }
}

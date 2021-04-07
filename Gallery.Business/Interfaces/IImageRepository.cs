using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface IImageRepository
    {
        public bool CreateImage(string title, string author, string format, byte[] data, List<Category> categories);
        public bool UpdateImage(int imageid, string title, string author, string format, byte[] data, List<Category> categories);
        public bool DeleteImage(int imageid);
        public List<Image> GetLastFiveImagesByCategory(int categoryid);
    }
}

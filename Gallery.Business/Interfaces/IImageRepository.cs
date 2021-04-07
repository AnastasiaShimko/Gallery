using System;
using System.Collections.Generic;
using System.Text;
using Gallery.Business.Models;

namespace Gallery.Business.Interfaces
{
    public interface IImageRepository
    {
        public bool CreateImage(string title, string author, string format, byte[] data, List<Category> categories);
    }
}

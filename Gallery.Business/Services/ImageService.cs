using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Gallery.Business.Models;
using Microsoft.AspNetCore.Http;

namespace Gallery.Business.Services
{
    public class ImageService
    {
        public async Task<Image> GetImageForCreate(IFormFile file, Image image, List<Category> categoriesList)
        {
            var fileStream = file.OpenReadStream();
            using var binaryReader = new BinaryReader(fileStream);
            var fileBytes = binaryReader.ReadBytes((Int32)fileStream.Length);
            var newImage = new Image()
            {
                Author = image.Author,
                Categories = categoriesList,
                Data = fileBytes,
                Format = file.ContentType,
                Title = image.Title
            };
            return newImage;
        }
    }
}

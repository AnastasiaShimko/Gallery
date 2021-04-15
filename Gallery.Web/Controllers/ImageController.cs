using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Gallery.Web.Controllers
{
    public class ImageController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IImageRepository _imageRepository;
        private readonly ILogger<HomeController> _logger;

        public ImageController(ILogger<HomeController> logger, IImageRepository imageRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int categoryid)
        {
            return View(_imageRepository.GetAllImagesByCategory(categoryid));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "ID", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormFile file, string title, string author, List<int> categories)
        {
            if (ModelState.IsValid)
            {
                var categoriesList = new List<Category>();
                foreach (var categoryId in categories)
                {
                    categoriesList.Add(_categoryRepository.GetCategoryById(categoryId));
                }
                var result = false;
                var fileStream = file.OpenReadStream();
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    var fileBytes = binaryReader.ReadBytes((Int32)fileStream.Length);
                    var newImage = new Image()
                    {
                        Author = author,
                        Categories = categoriesList,
                        Data = fileBytes,
                        Format = file.ContentType,
                        Title = title
                    };

                    result = _imageRepository.CreateImage(newImage);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "ID", "Name");
            return View(_imageRepository.GetImageById(id));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
               /* var imageDb = _imageRepository.GetImageById(image.ID);
                image.Data = imageDb.Data;
                image.Format = imageDb.Format;*/

                _imageRepository.UpdateImage(image);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var image = _imageRepository.GetImageById(id);
            ViewBag.Format = image.Format;
            ViewBag.Data = image.Data;
            return View(image);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Image image)
        {
            if (ModelState.IsValid)
            {
                _imageRepository.DeleteImage(image.ID);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

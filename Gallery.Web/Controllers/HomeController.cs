using Gallery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Image = Gallery.Business.Models.Image;

namespace Gallery.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private IImageRepository _imageRepository;
        private ICategoryRepository _categoryRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IImageRepository imageRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            var images = new List<Image>();
            foreach (var category in ViewBag.Categories)
            {
                List<Image> categoryImages = _imageRepository.GetLastFiveImagesByCategory(category.ID);
                if (categoryImages.Count > 0)
                {
                    foreach (var img in categoryImages)
                    {
                        img.Categories = new List<Category>(){category};
                    }
                    images.AddRange(categoryImages);
                }
            }
            ViewBag.Images = images.Distinct();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

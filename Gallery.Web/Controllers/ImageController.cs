using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Gallery.Business.Services;
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
        private readonly ImageService _imageService;
        private readonly ILogger<HomeController> _logger;

        public ImageController(ILogger<HomeController> logger, IImageRepository imageRepository, ICategoryRepository categoryRepository, ImageService imageService)
        {
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            return View(nameof(Index),await _imageRepository.SearchImagesByString(searchString));
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            return View(await _imageRepository.SearchImagesByString(searchString));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _imageRepository.GetAllImages());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int imageId)
        {
            try
            {
                var image = await _imageRepository.GetImageById(imageId);
                return View(image);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategories(), "ID", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormFile file, Image image, List<int> categories)
        {
            if (ModelState.IsValid)
            {
                var categoriesList = categories.Select(categoryId => _categoryRepository.GetCategoryById(categoryId)).ToList();
                var newImage = await _imageService.GetImageForCreate(file, image, categoriesList);
                await _imageRepository.CreateImage(newImage);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(image);
            }
        }

        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategories(), "ID", "Name");
            return View(await _imageRepository.GetImageById(id));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Image image, List<int> categories)
        {
            if (ModelState.IsValid)
            {
                var categoriesList = categories.Select(categoryId => _categoryRepository.GetCategoryById(categoryId)).ToList();
                image.Categories = (ICollection<Category>)categoriesList;
                await _imageRepository.UpdateImage(image);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var image = await _imageRepository.GetImageById(id);
            return View(image);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Image image)
        {
            await _imageRepository.DeleteImage(image.ID);
            return RedirectToAction(nameof(Index));
        }
    }
}

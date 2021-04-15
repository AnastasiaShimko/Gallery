using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Gallery.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private readonly ILogger<HomeController> _logger;

        public CategoryController(ILogger<HomeController> logger, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View(_categoryRepository.GetAllCategories());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.CreateCategory(category);
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            return View(_categoryRepository.GetCategoryById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(category);
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            return View(_categoryRepository.GetCategoryById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.DeleteCategory(category.ID);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

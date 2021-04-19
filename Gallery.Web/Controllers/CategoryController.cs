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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<HomeController> _logger;

        public CategoryController(ILogger<HomeController> logger, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories();
                return View(categories);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepository.CreateCategory(category);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(category);
                }
            }
            catch
            {
                return View(category);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var category = _categoryRepository.GetCategoryById(id);
                return View(category);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.UpdateCategory(category);
                }
                catch
                {
                    BadRequest();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                return View(_categoryRepository.GetCategoryById(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Category category)
        {
            try
            {
                await _categoryRepository.DeleteCategory(category.ID);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

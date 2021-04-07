﻿using Gallery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gallery.Business.Interfaces;
using Gallery.Business.Models;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Gallery.Web.Controllers
{
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file, string title, string author, List<Category> categories)
        {
            var fileStream = file.OpenReadStream();
            using (var binaryReader = new BinaryReader(fileStream))
            {
                var fileBytes = binaryReader.ReadBytes((Int32) fileStream.Length);
            }
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

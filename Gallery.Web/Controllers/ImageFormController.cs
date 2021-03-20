using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Web.Controllers
{
    public class ImageFormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

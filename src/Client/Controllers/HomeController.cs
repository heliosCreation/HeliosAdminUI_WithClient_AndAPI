using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MyStatusCode(int code)
        {
            switch (code)
            {
                case 403:
                    return RedirectToAction("AccessDenied", "Authorization");
                case 401:
                    return RedirectToAction("AccessDenied", "Authorization");
                case 404:
                    return RedirectToAction("AccessDenied", "Authorization");
                default:
                    return RedirectToAction("Error", "Home");
            };
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using ProjectCodeX.Models;
using System.Diagnostics;

namespace ProjectCodeX.Controllers
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

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel error = new ErrorViewModel 
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            };
            _logger.LogError($"Error with RequestId:{error.RequestId}", error);

            return View();
        }
    }
}
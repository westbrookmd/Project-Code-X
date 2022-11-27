using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjectCodeX.Data;
using ProjectCodeX.Models;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ProjectCodeX.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ILogger _logger;
        private readonly ProjectCodeXContext _dbContext;
        private readonly EventViewModel _viewModel;

        public EventController(ILogger<EventController> logger, ProjectCodeXContext dbContext, EventViewModel viewModel)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
        }

        public IActionResult Index()
        {
            _viewModel.Events = _dbContext.Events.ToList();
            return View(_viewModel);
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var eventDetail = _dbContext.Events.Find(id);
            if (eventDetail is not null)
            {
                _viewModel.EventDetail = eventDetail;
            }
            return View(_viewModel);
        }

        [HttpGet]
        public JsonResult EventsAsJson()
        {
            return Json(_dbContext.Events.ToArray());

        }
    }
}

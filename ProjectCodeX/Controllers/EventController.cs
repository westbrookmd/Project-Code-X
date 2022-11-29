using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjectCodeX.Data;
using ProjectCodeX.Models;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace ProjectCodeX.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger _logger;
        private readonly ProjectCodeXContext _dbContext;
        private readonly EventViewModel _viewModel;
        private readonly IEmailSender _emailSender;

        public EventController(ILogger<EventController> logger, ProjectCodeXContext dbContext, EventViewModel viewModel, IEmailSender emailSender)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            _viewModel.Events = _dbContext.Events.ToList();
            ViewBag.Status = "Calendar";
            return View(_viewModel);
        }

        public IActionResult Grid()
        {
            _viewModel.Events = _dbContext.Events.ToList();
            return View(_viewModel);
        }
        public IActionResult Register(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = _dbContext.Users.Find(userId).Email;
            var result = _emailSender.SendEmailAsync("You've been registered!", "You've been registered!", "You've been registered");
            result.Wait();
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
        public JsonResult EventsAsJson(DateTime start, DateTime end)
        {
            var dbEvents = _dbContext.Events.ToArray();
           

            var events = new List<EventCalendarDisplayModel>();

            for (int i = 0; i < dbEvents.Length; i++)
            {
                events.Add(new EventCalendarDisplayModel()
                {
                    id = dbEvents[i].EventId,
                    title = dbEvents[i].Name,
                    start = dbEvents[i].Date.Value.ToString(),
                    end = dbEvents[i].Date.Value.AddHours(1).ToString(),
                    allDay = false
                });
            }
            return Json(events.ToArray());
        }
    }
}

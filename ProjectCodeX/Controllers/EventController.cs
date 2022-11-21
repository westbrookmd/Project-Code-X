using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCodeX.Data;
using ProjectCodeX.Models;
using System.Diagnostics;
using System.Net;

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
        // GET: Event
        public IActionResult Index()
        {
            //Create a list of event samples
            List<Event> events = new();
            for (int i = 1; i <= 50; i++)
            {
                //Some random values but most of the values are the same
                events.Add(new Event()
                {
                    EventId = i,
                    EventName = "",
                    Date = DateTime.Now,
                    Location = "City, State",
                    EventType = "EventType",
                    Attendees = i * 10,
                    AmountRaised = i * 4,
                    Cost = i * 10,
                    Notes = "Example Notes"
                });
            }
            //Create our view model temporarily for testing. This will move into dependency injection (I think)
            _viewModel.Events = events;
            return View(_viewModel);
        }

        // GET: Event/Get/5
        public IActionResult Details(int id)
        {
            var eventDetail = _dbContext.Events.Find(id);
            if (eventDetail is not null)
            {
                _viewModel.EventDetail = eventDetail;
            }
            return View(_viewModel);
        }
    }
}

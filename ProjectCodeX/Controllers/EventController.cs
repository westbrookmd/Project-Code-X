using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCodeX.Models;

namespace ProjectCodeX.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }
        // GET: EventController
        public ActionResult Index()
        {
            //Create a list of event samples
            List<Event> events = new();
            for (int i = 1; i <= 50; i++)
            {
                //Some random values but most of the values are the same
                events.Add(new Event()
                {
                    EventID = i,
                    Date = DateTime.Now,
                    Location = "City, State",
                    Type = "EventType",
                    NumberOfAttendees = i * 10,
                    AmountRaised = i * 4,
                    Cost = i * 10,
                    Notes = "Example Notes"
                });
            }
            //Create our view model temporarily for testing. This will move into dependency injection (I think)
            EventViewModel eventViewModel = new EventViewModel()
            {
                Events = events,
            };
            

            return View(eventViewModel);
        }

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectCodeX.Models;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/{action=Index}/{id?}")]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
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
            ViewBag.Status = "Admin";
            _viewModel.Events = _dbContext.Events.ToList();
            return View(_viewModel);
        }

        public IActionResult Edit(int id)
        {
            var eventDetail = _dbContext.Events.Find(id);
            if (eventDetail is not null)
            {
                _viewModel.EventDetail = eventDetail;
                return View(_viewModel);
            }
            else
            {
                _viewModel.EventDetail = new();
                return View(_viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Event e)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Event eventDbObject = _dbContext.Events.FirstOrDefault(n => n.EventId == e.EventId);
                    if (eventDbObject is not null)
                    {
                        eventDbObject.Name = e.Name;
                        eventDbObject.Date = e.Date;
                        eventDbObject.Location = e.Location;
                        eventDbObject.EventType = e.EventType;
                        eventDbObject.Notes = e.Notes;

                        _dbContext.Events.Update(eventDbObject);
                        _dbContext.SaveChanges();
                        return Edit(eventDbObject.EventId);
                    }
                    else
                    {
                        //news object isn't in the database, create a new object
                        var result = _dbContext.Events.Add(e);
                        _dbContext.SaveChanges();
                        return Edit(result.Entity.EventId);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var eventDbObject = _dbContext.Events.Find(id);
                if (eventDbObject is not null)
                {
                    _viewModel.EventDetail = eventDbObject;
                    return View(_viewModel);
                } 
            }
            _viewModel.EventDetail = null;
            return View(_viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, bool confirmedDeletion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var eventDbObject = _dbContext.Events.Find(id);
                    if (eventDbObject is not null && confirmedDeletion)
                    {
                        _dbContext.Events.Remove(eventDbObject);
                        _dbContext.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(_viewModel);
                    }
                }
                return View(_viewModel);
            }
            catch
            {
                return View(_viewModel);
            }
        }
    }
}

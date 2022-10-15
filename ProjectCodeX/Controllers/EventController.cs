﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCodeX.Managers;
using ProjectCodeX.Models;
using System.Diagnostics;
using System.Net;

namespace ProjectCodeX.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ILogger _logger;
        private readonly EventMgr _mgr;
        private readonly EventViewModel _viewModel;

        public EventController(ILogger<EventController> logger, EventMgr mgr, EventViewModel viewModel)
        {
            _logger = logger;
            _mgr = mgr;
            _viewModel = viewModel;
        }
        // GET: Event
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
            _viewModel.Events = events;
            return View(_viewModel);
        }

        // GET: Event/Get/5
        public ActionResult Get(int id)
        {
            
            Event eventFromDb = _mgr.GetEvent(id);
            if (eventFromDb is not null)
            {
                _viewModel.Events = new()
                {
                    eventFromDb!
                };
                return View(_viewModel);
            }
            else
            {
                ModelState.AddModelError("Events", "Unable to retrieve event. Please try again.");
                _viewModel.Events = new();
                return View(_viewModel);
            }
            
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
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

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return View(_viewModel);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
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

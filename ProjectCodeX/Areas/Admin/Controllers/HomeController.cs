using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCodeX.Models;
using ProjectCodeX.Reports;
using QuestPDF.Fluent;
using SendGrid;
using System.Data;
using System.Diagnostics;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/{action=Index}/{id?}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjectCodeXContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ProjectCodeXContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.Status = "Admin";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Reports()
        {
            return View();
        }

        

        public FileContentResult MemberList()
        {
            //TODO:Only include members
            List<User> users = _dbContext.Users.ToList();
            MembershipDocument doc = new(users);
            return File(doc.GeneratePdf(), "application/pdf");
        }

        public FileContentResult DonorList()
        {
            List<Donation> donations = _dbContext.Donations.ToList();
            DonorDocument doc = new(donations);
            return File(doc.GeneratePdf(), "application/pdf");
        }

        public FileContentResult EventCalendar()
        {
            List<Event> events = _dbContext.Events.ToList();
            EventDocument doc = new(events);
            return File(doc.GeneratePdf(), "application/pdf");
        }

        public FileContentResult ContactList()
        {
            List<Contact> contacts = _dbContext.Contacts.ToList();
            ContactDocument doc = new(contacts);
            return File(doc.GeneratePdf(), "application/pdf");
        }

        public FileContentResult FundSummary()
        {
            List<Purchase> purchases = _dbContext.Purchases.ToList();
            FundDocument doc = new(purchases);
            return File(doc.GeneratePdf(), "application/pdf");
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
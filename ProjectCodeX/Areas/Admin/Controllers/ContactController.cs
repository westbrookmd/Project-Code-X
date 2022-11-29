using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCodeX.Models;
using System.Data;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/{action=Index}/{id?}")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ProjectCodeXContext _dbContext;
        private readonly ContactViewModel _viewModel;

        public ContactController(ILogger<ContactController> logger, ProjectCodeXContext dbContext, ContactViewModel viewModel)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
        }

        public IActionResult Index()
        {
            ViewBag.Status = "Admin";
            _viewModel.Contacts = _dbContext.Contacts.ToList();
            return View(_viewModel);
        }

        public IActionResult Edit(int id)
        {
            var contactDetail = _dbContext.Contacts.Find(id);
            if (contactDetail is not null)
            {
                _viewModel.ContactDetail = contactDetail;
                return View(_viewModel);
            }
            else
            {
                _viewModel.ContactDetail = new();
                return View(_viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contact contactDbObject = _dbContext.Contacts.FirstOrDefault(con => con.ContactId == contact.ContactId);
                    if (contactDbObject is not null)
                    {
                        contactDbObject.Fname = contact.Fname;
                        contactDbObject.Lname = contact.Lname;
                        contactDbObject.Company = contact.Company;
                        contactDbObject.Address = contact.Address;
                        contactDbObject.City = contact.City;
                        contactDbObject.State = contact.State;
                        contactDbObject.Phone = contact.Phone;
                        contactDbObject.Email = contact.Email;

                        _dbContext.Contacts.Update(contactDbObject);
                        _dbContext.SaveChanges();
                        return RedirectToAction("Index", "Contact", new {area="Admin"});
                    }
                    else
                    {
                        //news object isn't in the database, create a new object
                        var result = _dbContext.Contacts.Add(contact);
                        _dbContext.SaveChanges();
                        return RedirectToAction("Index", "Contact", new { area = "Admin" });
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
                var contactDbObject = _dbContext.Contacts.Find(id);
                if (contactDbObject is not null)
                {
                    _viewModel.ContactDetail = contactDbObject;
                    return View(_viewModel);
                }
            }
            _viewModel.ContactDetail = null;
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
                    var contactDbObject = _dbContext.Contacts.Find(id);
                    if (contactDbObject is not null && confirmedDeletion)
                    {
                        _dbContext.Contacts.Remove(contactDbObject);
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

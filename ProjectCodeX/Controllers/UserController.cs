using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Data;
using ProjectCodeX.Models;
using System.Security.Claims;

namespace ProjectCodeX.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ProjectCodeXContext _dbContext;
        private readonly UserViewModel _viewModel;

        public UserController(ILogger<UserController> logger, ProjectCodeXContext dbContext, UserViewModel viewModel)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
        }
        public IActionResult Index()
        {
            _viewModel.Users = null;
            var currentUserGUID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _viewModel.UserDetail = _dbContext.Users.Where(e => e.Id == currentUserGUID).FirstOrDefault();
            return View(_viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var userDbObject = _dbContext.Users.Find(currentUserId);
                    if (userDbObject is not null && user.Id == userDbObject.Id)
                    {
                        userDbObject.Fname = user.Fname;
                        userDbObject.Lname = user.Lname;
                        userDbObject.Address = user.Address;
                        userDbObject.City = user.City;
                        userDbObject.State = user.State;
                        _dbContext.Users.Update(userDbObject);
                        _dbContext.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}

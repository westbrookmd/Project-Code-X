using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Data;
using ProjectCodeX.Models;
using System.Security.Claims;

namespace ProjectCodeX.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserViewModel _viewModel;

        public UserController(ILogger<UserController> logger, ApplicationDbContext dbContext, UserViewModel viewModel)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
        }
        // GET: UserController
        public IActionResult Index()
        {
            _viewModel.Users = null;
            var currentUserGUID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _viewModel.UserDetail = _dbContext.User.Where(e => e.UserGUID == currentUserGUID).FirstOrDefault();
            return View(_viewModel);
        }

        // GET: UserController/Details/5
        public IActionResult Details(int id)
        {
            var userDetail = _dbContext.User.Find(id);
            if (userDetail is not null)
            {
                _viewModel.UserDetail = userDetail;
            }
            return View(_viewModel);
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            return View(_viewModel);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDBObject = _dbContext.User.Add(user);
                    return Details(userDBObject.Entity.UserID);
                }
                else
                {
                    return View(_viewModel);
                }

            }
            catch
            {
                return View(_viewModel);
            }
        }

        // GET: UserController/Edit/5
        public IActionResult Edit(int id)
        {
            var userDetail = _dbContext.User.Find(id);
            if (userDetail is not null)
            {
                _viewModel.UserDetail = userDetail;
                return Details(id);
            }
            else
            {
                return View(_viewModel);
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.User.Update(user);
                }
                return Details(id);
            }
            catch
            {
                return View(_viewModel);
            }
        }

        // GET: UserController/Delete/5
        public IActionResult Delete(string id)
        {
            var user = _dbContext.User.Find(id);
            if (user is not null)
            {
                _viewModel.UserDetail = user;
                return View(_viewModel);
            }
            _viewModel.UserDetail = new();
            return View(_viewModel);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, User user)
        {
            try
            {
                var userDbObject = _dbContext.User.Find(id);
                if (userDbObject is not null)
                {
                    _dbContext.User.Remove(userDbObject);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(_viewModel);
                }
            }
            catch
            {
                return View(_viewModel);
            }
        }
    }
}

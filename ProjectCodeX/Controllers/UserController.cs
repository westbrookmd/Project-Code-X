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

        public IActionResult Details(int id)
        {
            var userDetail = _dbContext.Users.Find(id);
            if (userDetail is not null)
            {
                _viewModel.UserDetail = userDetail;
            }
            return View(_viewModel);
        }

        public IActionResult Edit(int id)
        {
            var userDetail = _dbContext.Users.Find(id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Users.Update(user);
                }
                return Details(id);
            }
            catch
            {
                return View(_viewModel);
            }
        }
    }
}

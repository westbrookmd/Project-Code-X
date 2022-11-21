using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Controllers;
using ProjectCodeX.Data;
using ProjectCodeX.Models;
using System.Data;
using System.Security.Claims;

namespace ProjectCodeX.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("[area]/[controller]/{id?}")]
public class UserData : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly ProjectCodeXContext _dbContext;
    private readonly UserViewModel _viewModel;
    public UserData(ILogger<UserController> logger, ProjectCodeXContext dbContext, UserViewModel viewModel)
    {
        _logger = logger;
        _dbContext = dbContext;
        _viewModel = viewModel;
    }
    public IActionResult Index()
    {
        _viewModel.Users = _dbContext.Users.ToList();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _viewModel.UserDetail = _dbContext.Users.Where(e => e.UserGuid == userId).FirstOrDefault();
        return View(_viewModel);
    }
}

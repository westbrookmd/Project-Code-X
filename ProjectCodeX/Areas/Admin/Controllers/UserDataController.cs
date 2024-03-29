﻿using Microsoft.AspNetCore.Authorization;
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
[Route("[area]/[controller]/{action=Index}/{id?}")]
public class UserDataController : Controller
{
    private readonly ILogger<UserDataController> _logger;
    private readonly ProjectCodeXContext _dbContext;
    private readonly UserViewModel _viewModel;
    public UserDataController(ILogger<UserDataController> logger, ProjectCodeXContext dbContext, UserViewModel viewModel)
    {
        _logger = logger;
        _dbContext = dbContext;
        _viewModel = viewModel;
    }
    public IActionResult Index()
    {
        _viewModel.Users = _dbContext.Users.ToList();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _viewModel.UserDetail = _dbContext.Users.Where(e => e.Id == userId).FirstOrDefault();
        return View(_viewModel);
    }

    public IActionResult Edit(string id)
    {
        var userDetail = _dbContext.Users.Find(id);
        if (userDetail is not null)
        {
            _viewModel.UserDetail = userDetail;
            return View(_viewModel);
        }
        else
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(_viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(User user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                //pull object first to begin tracking
                var userDBObject = _dbContext.Users.FirstOrDefault(e => e.Id == user.Id);
                if (userDBObject is not null)
                {
                    userDBObject.Fname = user.Fname;
                    userDBObject.Lname = user.Lname;
                    userDBObject.Address = user.Address;
                    userDBObject.City = user.City;
                    userDBObject.State = user.State;

                    _dbContext.Users.Update(userDBObject);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("Index", "UserData", new { area="Admin" });
            }
            return View(_viewModel);
        }
        catch
        {
            return View(_viewModel);
        }
    }

    public IActionResult Delete(string id)
    {
        var user = _dbContext.Users.Find(id);
        if (user is not null)
        {
            _viewModel.UserDetail = user;
            return View(_viewModel);
        }

        return View(_viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(string id, bool confirmedDeletion)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var userDbObject = _dbContext.Users.Find(id);
                if (userDbObject is not null && confirmedDeletion)
                {
                    _dbContext.Users.Remove(userDbObject);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
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

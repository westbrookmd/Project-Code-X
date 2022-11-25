using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using ProjectCodeX.Models;
using System.Data;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/{action=Index}/{id?}")]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly ProjectCodeXContext _dbContext;
        private readonly GroupViewModel _viewModel;
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public GroupController(ILogger<GroupController> logger, ProjectCodeXContext dbContext, GroupViewModel viewModel, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles =  _roleManager.Roles.ToList();
            foreach (IdentityRole role in roles)
            {
                var usersInRole = _userManager.GetUsersInRoleAsync(role.Name).Result;
                if (usersInRole is not null)
                {
                    Group group = new(role.Id,
                                      role.Name,
                                      usersInRole.Count,
                                      usersInRole.ToList());
                    _viewModel.Groups.Add(group);
                }
            }
            return View(_viewModel);
        }

        public IActionResult Add()
        {
            return Edit();
        }

        public IActionResult Edit(string id = "")
        {
            var role = _roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (role is not null)
            {
                var usersInRole = _userManager.GetUsersInRoleAsync(role.Name).Result;
                if (usersInRole is not null)
                {
                    Group group = new(role.Id,
                                        role.Name,
                                        usersInRole.Count,
                                        usersInRole.ToList());

                    _viewModel.GroupDetail = group;
                } 
            }
            return View(_viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, string name)
        {
            try
            {
                var existingRoleObject = _roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
                if (existingRoleObject is not null)
                {
                    _roleManager.SetRoleNameAsync(existingRoleObject, name);
                }
                else
                {
                    IdentityRole role = new(name);
                    var roleCreated = _roleManager.CreateAsync(role).Result;
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var role = _roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (role is not null)
            {
                var roleDeleted = _roleManager.DeleteAsync(role).Result;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UserEdit(string id)
        {
            var role = _roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (role is not null)
            {
                var usersInRole = _userManager.GetUsersInRoleAsync(role.Name).Result;
                if (usersInRole is not null)
                {
                    Group group = new(role.Id,
                                        role.Name,
                                        usersInRole.Count,
                                        usersInRole.ToList());

                    _viewModel.GroupDetail = group;
                }
            }
            return View(_viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserEdit(string id, string groupId, string groupName)
        {
            try
            {
                var existingUser = _userManager.Users.Where(r => r.Id == id).FirstOrDefault();
                if (existingUser is not null)
                {
                    var usersInRole = _userManager.GetUsersInRoleAsync(groupName).Result;
                    if (usersInRole is not null)
                    {
                        var ourUser = usersInRole.Where(u => u.Id == id).FirstOrDefault();
                        if (ourUser is not null)
                        {
                            var removedSucessfully = _userManager.RemoveFromRoleAsync(existingUser, groupName).Result;
                        }
                        else
                        {
                            var userAddedToRole = _userManager.AddToRoleAsync(existingUser, groupName).Result;
                        }
                    }
                    
                    //_viewModel.Groups.Find(g => g.Id == groupId)?.Users.Add(existingUser);
                }
                
                return UserEdit(groupId);
            }
            catch
            {
                return UserEdit(groupId);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult UserDelete(string id, string groupId, string groupName)
        //{
        //    try
        //    {
        //        var existingUser = _userManager.Users.Where(r => r.Id == id).FirstOrDefault();
        //        if (existingUser is not null)
        //        {
        //            var userAddedToRole = _userManager.RemoveFromRoleAsync(existingUser, groupName).Result;
        //            //_viewModel.Groups.Find(g => g.Id == groupId)?.Users.Remove(existingUser);
        //        }

        //        return UserEdit(groupId);
        //    }
        //    catch
        //    {
        //        return UserEdit(groupId);
        //    }
        //}

        
    }
}

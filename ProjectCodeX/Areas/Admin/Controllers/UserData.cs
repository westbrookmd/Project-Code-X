using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class UserData : Controller
    {
        [Route("[area]/[controller]/{id?}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

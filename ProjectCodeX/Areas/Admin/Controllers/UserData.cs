using Microsoft.AspNetCore.Mvc;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UserData : Controller
    {
        [Route("[area]/[controller]/{id?}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

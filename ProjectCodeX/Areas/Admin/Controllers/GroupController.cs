using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GroupController : Controller
    {
        [Route("[area]/[controller]/{id?}")]

        public IActionResult Index()
        {
            return View();
        }

        // GET: GroupController/Details/5
        public IActionResult Details(int id)
        {

            return View();
        }

        // GET: GroupController/Create
        public IActionResult Add()
        {
            return Edit(0);
        }

        // POST: GroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: GroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

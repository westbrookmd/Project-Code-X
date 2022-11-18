using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectCodeX.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }
        // GET: News
        public IActionResult Index()
        {
            return View();
        }

        // GET: News/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}

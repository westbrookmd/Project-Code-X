using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectCodeX.Data;
using ProjectCodeX.Models;

namespace ProjectCodeX.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly NewsViewModel _viewModel;

        public NewsController(ILogger<NewsController> logger, ApplicationDbContext dbContext, NewsViewModel viewModel)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
        }
        // GET: News
        public IActionResult Index()
        {
            _viewModel.Posts = _dbContext.Posts.ToList();
            return View(_viewModel);
        }

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            var postDetail = _dbContext.Posts.Find(id);
            if (postDetail is not null)
            {
                _viewModel.PostDetail = postDetail;
            }
            return View(_viewModel);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            
            return View(_viewModel);
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsPost newsPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var post = _dbContext.Posts.Add(newsPost);
                    return Details(post.Entity.ArticleID);
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

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            var postDetail = _dbContext.Posts.Find(id);
            if (postDetail is not null)
            {
                _viewModel.PostDetail = postDetail;
                return Details(id);
            }
            else
            {
                return View(_viewModel);
            }
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NewsPost newsPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Posts.Update(newsPost);
                }
                return Details(id);
            }
            catch
            {
                return View(_viewModel);
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            var post = _dbContext.Posts.Find(id);
            if (post is not null)
            {
                _viewModel.PostDetail = post;
                return View(_viewModel);
            }
            _viewModel.PostDetail = new();
            return View(_viewModel);
        }

        // POST: News/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var post = _dbContext.Posts.Find(id);
                if (post is not null)
                {
                    _dbContext.Posts.Remove(post);
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

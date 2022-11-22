using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectCodeX.Data;
using ProjectCodeX.Models;

namespace ProjectCodeX.Controllers;

public class NewsController : Controller
{
    private readonly ILogger<NewsController> _logger;
    private readonly ProjectCodeXContext _dbContext;
    private readonly NewsViewModel _viewModel;

    public NewsController(ILogger<NewsController> logger, ProjectCodeXContext dbContext, NewsViewModel viewModel)
    {
        _logger = logger;
        _dbContext = dbContext;
        _viewModel = viewModel;
    }
    // GET: News
    public IActionResult Index()
    {
        _viewModel.News = _dbContext.News.ToList();
        return View(_viewModel);
    }

    // GET: News/Details/5
    public ActionResult Article(int id)
    {
        var postDetail = _dbContext.News.Find(id);
        if (postDetail is not null)
        {
            _viewModel.NewsDetail = postDetail;
            return View(_viewModel);
        }
        else
        {
            return View(_viewModel);
        }
    }
}
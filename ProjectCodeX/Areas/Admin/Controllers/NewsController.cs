using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Models;
using System.Data;

namespace ProjectCodeX.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
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
    [Route("[area]/[controller]/{id?}")]

    public IActionResult Index()
    {
        _viewModel.News = _dbContext.News.ToList();
        return View(_viewModel);
    }

    // GET: News/Details/5
    public ActionResult Details(int id)
    {
        var postDetail = _dbContext.News.Find(id);
        if (postDetail is not null)
        {
            _viewModel.NewsDetail = postDetail;
        }
        return View(_viewModel);
    }

    // GET: News/Edit/5
    public IActionResult Edit(int id)
    {
        var postDetail = _dbContext.News.Find(id);
        if (postDetail is not null)
        {
            _viewModel.NewsDetail = postDetail;
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
    public IActionResult Edit(int id, News newsPost)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _dbContext.News.Update(newsPost);
            }
            return Details(id);
        }
        catch
        {
            return View(_viewModel);
        }
    }

    // GET: News/Delete/5
    public IActionResult Delete(int id)
    {
        var post = _dbContext.News.Find(id);
        if (post is not null)
        {
            _viewModel.NewsDetail = post;
            return View(_viewModel);
        }
        _viewModel.NewsDetail = new();
        return View(_viewModel);
    }

    // POST: News/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            var post = _dbContext.News.Find(id);
            if (post is not null)
            {
                _dbContext.News.Remove(post);
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

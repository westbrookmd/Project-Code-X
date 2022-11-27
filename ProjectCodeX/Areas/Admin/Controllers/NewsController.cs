using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Models;
using System.Data;

namespace ProjectCodeX.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("[area]/[controller]/{action=Index}/{id?}")]
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
    

    public IActionResult Index()
    {
        _viewModel.News = _dbContext.News.ToList();
        return View(_viewModel);
    }

    public IActionResult Edit(int id)
    {
        var postDetail = _dbContext.News.Find(id);
        if (postDetail is not null)
        {
            ViewBag.Status = "Edit";
            _viewModel.NewsDetail = postDetail;
            return View(_viewModel);
        }
        else
        {
            _viewModel.NewsDetail = new();
            return View(_viewModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, News news)
    {
        try
        {
            if (ModelState.IsValid)
            {
                News newsDBObject = _dbContext.News.FirstOrDefault(e => e.ArticleId == news.ArticleId);
                if (newsDBObject is not null && news.ArticleId is not 0)
                {
                    newsDBObject.Summary = news.Summary;
                    newsDBObject.ViewCount = news.ViewCount;
                    newsDBObject.Author = news.Author;

                    _dbContext.News.Update(newsDBObject);
                    _dbContext.SaveChanges();
                    return Edit(id);
                }
                else
                {
                    //news object isn't in the database, create a new object
                    news.PublishDate = DateTime.Now;
                    var result = _dbContext.News.Add(news);
                    _dbContext.SaveChanges();
                    return Edit(result.Entity.ArticleId);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id, bool confirmedDeletion)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var post = _dbContext.News.Find(id);
                if (post is not null && confirmedDeletion)
                {
                    _dbContext.News.Remove(post);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(_viewModel);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectCodeX.Data.Migrations;
using ProjectCodeX.Models;
using System.Security.Claims;

namespace ProjectCodeX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/{action=Index}/{id?}")]

    public class FundsController : Controller
    {
        private readonly FundViewModel _viewModel;
        private readonly ProjectCodeXContext _dbContext;

        public FundsController(FundViewModel viewModel, ProjectCodeXContext dbContext)
        {
            _viewModel = viewModel;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            _viewModel.Purchases = _dbContext.Purchases.ToList();
            return View(_viewModel);
        }

        public IActionResult Edit(int id)
        {
            var purchase = _dbContext.Purchases.Find(id);
            if (purchase is not null)
            {
                ViewBag.Status = "Edit";
                _viewModel.PurchaseDetail = purchase;
                return View(_viewModel);
            }
            else
            {
                _viewModel.PurchaseDetail = new();
                return View(_viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Purchase purchase)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (ModelState.IsValid)
                {
                    Purchase purchaseDbObject = _dbContext.Purchases.FirstOrDefault(e => e.PurchId == purchase.PurchId);
                    if (purchaseDbObject is not null && purchase.PurchId is not 0)
                    {
                        //purchaseDbObject.UserId = //TODO
                        purchaseDbObject.Total = purchase.Total;
                        purchaseDbObject.PurchDate = purchase.PurchDate;
                        purchaseDbObject.Notes = purchase.Notes;
                        purchaseDbObject.PurchName = purchase.PurchName;
                        purchaseDbObject.Qnty = purchase.Qnty;
                        purchaseDbObject.Price = purchase.Price;

                        purchaseDbObject.UserId = userId;
                        _dbContext.Purchases.Update(purchaseDbObject);
                        _dbContext.SaveChanges();
                        return RedirectToAction("Index", "Funds", new { area = "Admin" });
                    }
                    else
                    {
                        //news object isn't in the database, create a new object
                        purchase.UserId = userId;
                        var result = _dbContext.Purchases.Add(purchase);
                        _dbContext.SaveChanges();
                        return RedirectToAction("Index", "Funds", new { area = "Admin" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Delete(int id)
        {
            var purchase = _dbContext.Purchases.Find(id);
            if (purchase is not null)
            {
                _viewModel.PurchaseDetail = purchase;
                return View(_viewModel);
            }
            _viewModel.PurchaseDetail = new();
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
                    var purchase = _dbContext.Purchases.Find(id);
                    if (purchase is not null && confirmedDeletion)
                    {
                        _dbContext.Purchases.Remove(purchase);
                        _dbContext.SaveChanges();
                        return RedirectToAction("Index");
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
}

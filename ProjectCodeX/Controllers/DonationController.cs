using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCodeX.Models;
using System.Security.Claims;

namespace ProjectCodeX.Controllers
{
    [Authorize]
    public class DonationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ProjectCodeXContext _dbContext;
        private readonly DonationViewModel _viewModel;

        public DonationController(ILogger<DonationController> logger, ProjectCodeXContext dbContext, DonationViewModel viewModel)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
        }

        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _viewModel.Donations = _dbContext.Donations.Where(d => d.UserId == currentUserId).ToList();
            if (_viewModel.Donations is null)
            {
                _viewModel.Donations = new List<Donation>();
            }
            return View(_viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Donate(Donation donation)
        {
            try
            {
                donation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                donation.DonationDate = DateTime.UtcNow;
                var result = _dbContext.Donations.Add(donation);
                _dbContext.SaveChanges();
                RedirectToAction("Index");
                //return Edit(result.Entity.DonationId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

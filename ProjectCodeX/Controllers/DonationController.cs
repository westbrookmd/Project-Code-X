using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public DonationController(ILogger<DonationController> logger, ProjectCodeXContext dbContext, DonationViewModel viewModel, UserManager<User> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _viewModel = viewModel;
            _userManager = userManager;
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
        public IActionResult Donate(Donation donation, bool becomeMember, int dueTier)
        {
            try
            {
                donation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                donation.DonationDate = DateTime.UtcNow;
                var donationAdded = _dbContext.Donations.Add(donation);

                User user = _dbContext.Users.Where(u => u.Id == donation.UserId).FirstOrDefault();
                if (user is not null)
                {
                    if (donation.Amount.HasValue)
                    {
                        user.Balance -= donation.Amount.Value;
                        if (_userManager.IsInRoleAsync(user, "Donor").Result)
                        {
                            var donorRoleAdded = _userManager.AddToRoleAsync(user, "Donor").Result;
                        }  
                    }
                    //Member joining
                    if (becomeMember)
                    {
                        var memberAddedSuccessfully = _userManager.AddToRoleAsync(user, "Member").Result;
                        if (memberAddedSuccessfully.Succeeded)
                        {
                            user.NextBillDate = DateTime.Now.AddDays(30);
                            user.DueTier = dueTier;
                        }
                    }
                    _dbContext.Users.Update(user);
                }
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Unsubscribe(string userId)
        {
            User user = _dbContext.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user is not null)
            {
                var removed = _userManager.RemoveFromRoleAsync(user, "Member").Result;
                if (removed.Succeeded)
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }
    }
}

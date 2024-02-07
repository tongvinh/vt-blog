using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VTBlog.Core.Domain.Identity;
using VTBlog.Core.SeedWorks;
using VTBlog.Core.SeedWorks.Constants;
using VTBlog.WebApp.Extensions;
using VTBlog.WebApp.Models;

namespace VTBlog.WebApp.Controllers
{
    [Authorize]
    public class ProfileController(IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;

        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            return View(new ProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            });
        }

        [HttpGet]
        [Route("/profile/edit")]
        public async Task<IActionResult> ChangeProfile()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return View(new ChangeProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        [Route("/profile/edit")]
        [HttpPost]
        public async Task<IActionResult> ChangeProfile([FromForm] ChangeProfileViewModel model)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "Update profile successful.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Update profile failed");

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();

            return Redirect(UrlConsts.Home);
        }
    }
}

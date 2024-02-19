using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using VTBlog.Core.ConfigOptions;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Domain.Identity;
using VTBlog.Core.Helpers;
using VTBlog.Core.SeedWorks;
using VTBlog.Core.SeedWorks.Constants;
using VTBlog.WebApp.Extensions;
using VTBlog.WebApp.Models;

namespace VTBlog.WebApp.Controllers
{
    [Authorize]
    public class ProfileController(IUnitOfWork unitOfWork, 
        SignInManager<AppUser> signInManager, 
        UserManager<AppUser> userManager,
        IOptions<SystemConfig> config) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SystemConfig _config = config.Value;

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
            var user = await GetCurrentUser();
            return View(new ChangeProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        [Route("/profile/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeProfile([FromForm] ChangeProfileViewModel model)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData[SystemConsts.FormSuccessMsg] = "Update profile successful.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Update profile failed");

            }
            return View(model);
        }

        [Route("profile/change-password")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("profile/change-password")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userProfile = await GetCurrentUser();

            var isPasswordValid = await _userManager.CheckPasswordAsync(userProfile, model.OldPassword);
            if (!isPasswordValid)
            {
                ModelState.AddModelError(string.Empty, "Old password is not correct");
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(userProfile, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(userProfile);
                TempData[SystemConsts.FormSuccessMsg] = "Change password successful";
                return Redirect(UrlConsts.Profile);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
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

        private async Task<AppUser> GetCurrentUser()
        {
            var userId = User.GetUserId();
            return await _userManager.FindByIdAsync(userId.ToString());
        }


        [HttpGet]
        [Route("/profile/posts/create")]
        public async Task<IActionResult> CreatePost()
        {
            return View(await SetCreatePostModel());
        }

        [Route("/profile/posts/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostViewModel model, IFormFile thumbnail)
        {
            if (!ModelState.IsValid)
            {
                return View(await SetCreatePostModel());
            }

            var user = await GetCurrentUser();
            var category = await _unitOfWork.PostCategories.GetByIdAsync(model.CategoryId);
            var post = new Post()
            {
                Name = model.Title,
                CategoryName = category.Name,
                CategorySlug = category.Slug,
                Slug = TextHelper.ToUnsignedString(model.Title),
                CategoryId = model.CategoryId,
                Content = model.Content,
                SeoDescription = model.SeoDescription,
                Status = PostStatus.Draft,
                AuthorUserId = user.Id,
                AuthorName = user.GetFullName(),
                AuthorUserName = user.UserName,
                Description = model.Description
            };
            _unitOfWork.Posts.Add(post);
            if (thumbnail != null)
            {
                await UploadThumbnail(thumbnail, post);
            }

            int result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                TempData[SystemConsts.FormSuccessMsg] = "Post is created successful.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Create post failed");
            }
            return View(model);
        }

        private async Task UploadThumbnail(IFormFile thumbnail, Post post)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.BackendApiUrl);

                byte[] data;
                using (var br = new BinaryReader(thumbnail.OpenReadStream()))
                {
                    data = br.ReadBytes((int) thumbnail.OpenReadStream().Length);
                }

                var bytes = new ByteArrayContent(data);

                var multiContent = new MultipartFormDataContent()
                {
                    {bytes, "file", thumbnail.FileName}
                };

                var uploadResult = await client.PostAsync("api/admin/media?type=posts", multiContent);
                if (uploadResult.StatusCode != HttpStatusCode.OK)
                {
                    ModelState.AddModelError("", await uploadResult.Content.ReadAsStringAsync());
                }
                else
                {
                    var path = await uploadResult.Content.ReadAsStringAsync();
                    var pathObj = JsonSerializer.Deserialize<UploadResponse>(path);
                    post.Thumbnail = pathObj?.Path;
                }
            }
        }

        private async Task<CreatePostViewModel> SetCreatePostModel()
        {
            var model = new CreatePostViewModel()
            {
                Title = "Untitled",
                Categories = new SelectList(await _unitOfWork.PostCategories.GetAllAsync(), "Id", "Name")
            };
            return model;
        }

        [HttpGet]
        [Route("/profile/posts/list")]
        public async Task<IActionResult> ListPosts(string keyword, int page = 1)
        {
            var posts = await _unitOfWork.Posts.GetPostByUserPaging(keyword, User.GetUserId(), page, 12);
            return View(new ListPostByUserViewModel() {Posts = posts});
        }

    }
}

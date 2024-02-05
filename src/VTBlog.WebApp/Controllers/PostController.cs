﻿using Microsoft.AspNetCore.Mvc;
using VTBlog.Core.SeedWorks;
using VTBlog.WebApp.Models;

namespace VTBlog.WebApp.Controllers
{
    public class PostController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [Route("posts")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("posts/{categorySlug}")]
        public async Task<IActionResult> ListByCategory([FromRoute] string categorySlug, [FromQuery] int page = 1)
        {
            var posts = await _unitOfWork.Posts.GetPostByCategoryPaging(categorySlug, page, 2);
            var category = await _unitOfWork.PostCategories.GetBySlug(categorySlug);
            return View(new PostListByCategoryViewModel()
            {
                Posts = posts,
                Category = category
            });
        }

        [Route("tag/{tagSlug}")]
        public IActionResult ListByTag([FromRoute] string tagSlug, [FromQuery] int page = 1)
        {
            return View();
        }

        [Route("post/{slug}")]
        public async Task<IActionResult> Details([FromRoute] string slug)
        {
            var post = await _unitOfWork.Posts.GetBySlug(slug);
            var category = await _unitOfWork.PostCategories.GetBySlug(post.CategorySlug);
            
            return View(new PostDetailViewModel()
            {
                Post = post,
                Category = category
            });
        }
    }
}

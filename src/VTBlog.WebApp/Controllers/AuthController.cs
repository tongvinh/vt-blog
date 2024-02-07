﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VTBlog.Core.Domain.Identity;
using VTBlog.Core.SeedWorks.Constants;
using VTBlog.WebApp.Models;
using VTBlog.Core.Events.LoginSuccessed;
using VTBlog.Core.Events.RegisterSuccessed;

namespace VTBlog.WebApp.Controllers
{
    public class AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IMediator mediator) : Controller
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userManager.CreateAsync(new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            }, model.Password);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                await _signInManager.SignInAsync(user, true);
                await _mediator.Publish(new RegisterSuccessedEvent(model.Email));
                return Redirect(UrlConsts.Profile);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty,"Email not found!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _mediator.Publish(new LoginSuccessedEvent(model.Email));

                return Redirect(UrlConsts.Profile);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login failed");
            }

            return View();
        }
    }
}

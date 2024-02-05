﻿using Microsoft.AspNetCore.Mvc;
using VTBlog.Core.Models;

namespace VTBlog.WebApp.Components
{
    public class PagerViewComponent:ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult) View("Default", result));
        }
    }
}

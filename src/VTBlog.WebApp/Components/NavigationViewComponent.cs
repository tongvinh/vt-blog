using Microsoft.AspNetCore.Mvc;
using VTBlog.Core.SeedWorks;
using VTBlog.WebApp.Models;

namespace VTBlog.WebApp.Components
{
    public class NavigationViewComponent(IUnitOfWork unitOfWork): ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _unitOfWork.PostCategories.GetAllAsync();
            var navItems = model.Select(x => new NavigationItemViewModel()
            {
                Slug = x.Slug,
                Name = x.Name,
                Children = model.Where(x => x.ParentId == x.Id).Select(i => new NavigationItemViewModel()
                {
                    Name = i.Name,
                    Slug = i.Slug,
                }).ToList()
            }).ToList();
            return View(navItems);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace VTBlog.WebApp.Components
{
    public class NavigationViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

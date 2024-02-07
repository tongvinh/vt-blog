using Microsoft.AspNetCore.Mvc;
using VTBlog.WebApp.Controllers;

namespace VTBlog.WebApp.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AuthController.ResetPassword),
                controller: "Auth",
                values: new { userId, code },
                protocol: scheme)
        }
    }
}

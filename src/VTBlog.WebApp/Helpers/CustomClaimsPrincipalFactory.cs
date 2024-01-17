using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VTBlog.Core.Domain.Identity;

namespace VTBlog.WebApp.Helpers
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }
    }
}

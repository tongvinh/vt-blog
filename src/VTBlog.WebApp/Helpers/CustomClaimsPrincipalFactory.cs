using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VTBlog.Core.Domain.Identity;
using VTBlog.Core.SeedWorks.Constants;

namespace VTBlog.WebApp.Helpers
{
    public class CustomClaimsPrincipalFactory(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IOptions<IdentityOptions> options)
        : UserClaimsPrincipalFactory<AppUser, AppRole>(userManager, roleManager, options)
    {

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
           var principal = await base.CreateAsync(user);
            //Add your claims here
           ((ClaimsIdentity) principal.Identity)?.AddClaims(new[] {
                new Claim(UserClaims.Id, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(UserClaims.FirstName, user.FirstName),
           });
            return principal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTBlog.Core.Domain.Identity;
using VTBlog.Core.Repositories;
using VTBlog.Data.SeedWorks;

namespace VTBlog.Data.Repositories
{
    public class UserRepository(VTBlogContext context) : RepositoryBase<AppUser, Guid>(context), IUserRepository
    {
        public async Task RemoveUserFromRoles(Guid userId, string[] roles)
        {
            if(roles == null || roles.Length == 0)
                return;
            foreach (var roleName in roles)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == roleName);
                if (role == null)
                {
                    return;
                }

                var userRole =
                    await _context.UserRoles.FirstOrDefaultAsync(x => x.RoleId == role.Id && x.UserId == userId);
                if (userRole == null)
                {
                    return;
                }

                _context.UserRoles.Remove(userRole);
            }
        }
    }
}

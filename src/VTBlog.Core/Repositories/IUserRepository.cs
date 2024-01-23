using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBlog.Core.Domain.Identity;
using VTBlog.Core.SeedWorks;

namespace VTBlog.Core.Repositories
{
    public interface IUserRepository:IRepository<AppUser, Guid>
    {
        Task RemoveUserFromRoles(Guid userId, string[] roles);
    }
}

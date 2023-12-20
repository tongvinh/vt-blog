using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.SeedWorks;

namespace VTBlog.Core.Repositories
{
    public interface IPostRepository:IRepository<Post, Guid>
    {
        Task<List<Post>> GetPopularPostAsync(int count);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Repositories;
using VTBlog.Data.SeedWorks;

namespace VTBlog.Data.Repositories
{
    public class PostRepository(VTBlogContext context) : RepositoryBase<Post, Guid>(context), IPostRepository
    {
        public async Task<List<Post>> GetPopularPostAsync(int count)
        {
            return await _context.Posts.OrderByDescending(x => x.ViewCount).Take(count).ToListAsync();
        }
    }
}

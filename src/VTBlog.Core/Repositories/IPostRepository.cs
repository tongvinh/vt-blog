using VTBlog.Core.Domain.Content;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;
using VTBlog.Core.SeedWorks;

namespace VTBlog.Core.Repositories
{
    public interface IPostRepository:IRepository<Post, Guid>
    {
        Task<List<Post>> GetPopularPostAsync(int count);
        Task<PagedResult<PostInListDto>> GetPostsPagingAsync(string? keyword, Guid? categoryId, int pageIndex = 1, int pageSize = 10);
    }
}

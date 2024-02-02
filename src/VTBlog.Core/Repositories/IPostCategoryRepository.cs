using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;
using VTBlog.Core.SeedWorks;

namespace VTBlog.Core.Repositories
{
    public interface IPostCategoryRepository: IRepository<PostCategory, Guid>
    {
        Task<PagedResult<PostCategoryDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
        Task<bool> HasPost(Guid categoryId);
    }
}

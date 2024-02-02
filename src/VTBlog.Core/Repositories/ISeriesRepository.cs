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
    public interface ISeriesRepository:IRepository<Series, Guid>
    {
        Task<PagedResult<SeriesInListDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
        Task AddPostToSeries(Guid seriesId, Guid postId, int sortOrder);
        Task RemovePostToSeries(Guid seriesId, Guid postId);
        Task<List<PostInListDto>> GetAllPostsInSeries(Guid seriesId);
        Task<bool> IsPostInSeries(Guid seriesId, Guid postId);
        Task<bool> HasPost(Guid seriesId);
    }
}

using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;

namespace VTBlog.WebApp.Models
{
    public class SeriesDetailViewModel
    {
        public SeriesDto Series { get; set; }
        public PagedResult<PostInListDto> Posts { get; set; }
    }
}

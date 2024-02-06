using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;

namespace VTBlog.WebApp.Models
{
    public class PostListByTagViewModel
    {
        public TagDto Tag { get; set; }
        public PagedResult<PostInListDto> Posts { get; set; }
    }
}

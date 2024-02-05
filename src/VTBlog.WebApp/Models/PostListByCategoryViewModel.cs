using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;

namespace VTBlog.WebApp.Models
{
    public class PostListByCategoryViewModel
    {
        public PostCategoryDto Category { get; set; }
        public PagedResult<PostInListDto> Posts { get; set; }
    }
}

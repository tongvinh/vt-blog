using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;
using VTBlog.Core.Repositories;
using VTBlog.Data.SeedWorks;

namespace VTBlog.Data.Repositories
{
    public class PostCategoryRepository(VTBlogContext context, IMapper mapper) : RepositoryBase<PostCategory, Guid>(context), IPostCategoryRepository
    {
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResult<PostCategoryDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.PostCategories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            return new PagedResult<PostCategoryDto>()
            {
                Results = await _mapper.ProjectTo<PostCategoryDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }
    }
}

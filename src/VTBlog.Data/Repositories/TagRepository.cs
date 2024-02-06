using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Models.Content;
using VTBlog.Core.Repositories;
using VTBlog.Data.SeedWorks;

namespace VTBlog.Data.Repositories
{
    public class TagRepository(IMapper mapper, VTBlogContext context) : RepositoryBase<Tag, Guid>(context), ITagRepository
    {
        private readonly IMapper _mapper = mapper;

        public async Task<TagDto> GetBySlug(string slug)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Slug == slug);
            if (tag == null) return null;
            return _mapper.Map<TagDto>(tag);
        }
    }
}

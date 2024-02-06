using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Models.Content;
using VTBlog.Core.SeedWorks;

namespace VTBlog.Core.Repositories
{
    public interface ITagRepository:IRepository<Tag, Guid>
    {
        Task<TagDto> GetBySlug(string slug);
    }
}

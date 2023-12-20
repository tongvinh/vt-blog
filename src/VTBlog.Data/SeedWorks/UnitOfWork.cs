using AutoMapper;
using VTBlog.Core.Repositories;
using VTBlog.Core.SeedWorks;
using VTBlog.Data.Repositories;

namespace VTBlog.Data.SeedWorks
{
    public class UnitOfWork(VTBlogContext context, IMapper mapper) : IUnitOfWork
    {
        private readonly VTBlogContext _context = context;

        public IPostRepository Posts { get; private set; } = new PostRepository(context, mapper);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VTBlog.Core.Domain.Identity;
using VTBlog.Core.Repositories;
using VTBlog.Core.SeedWorks;
using VTBlog.Data.Repositories;

namespace VTBlog.Data.SeedWorks
{
    public class UnitOfWork(VTBlogContext context, IMapper mapper,UserManager<AppUser> userManager) : IUnitOfWork
    {
        private readonly VTBlogContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;

        public IPostRepository Posts { get; private set; } = new PostRepository(context, mapper,userManager);

        public IUserRepository Users {get; private set;} = new UserRepository(context);

        public IPostCategoryRepository PostCategories { get; private set; } = new PostCategoryRepository(context,mapper);

        public ISeriesRepository Series { get; private set; } = new SeriesRepository(context, mapper);

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

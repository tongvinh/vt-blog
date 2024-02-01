using VTBlog.Core.Repositories;

namespace VTBlog.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        IPostCategoryRepository PostCategories { get; }
        IUserRepository Users { get; }
        ISeriesRepository Series { get; }
        Task<int> CompleteAsync();
    }
}

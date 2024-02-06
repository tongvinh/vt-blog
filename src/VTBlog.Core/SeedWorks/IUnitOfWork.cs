using VTBlog.Core.Repositories;

namespace VTBlog.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        IPostCategoryRepository PostCategories { get; }
        IUserRepository Users { get; }
        ISeriesRepository Series { get; }
        ITransactionRepository Transaction { get; }
        ITagRepository Tags { get; }

        Task<int> CompleteAsync();
    }
}

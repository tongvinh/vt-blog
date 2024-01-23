using VTBlog.Core.Repositories;

namespace VTBlog.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}

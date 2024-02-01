using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBlog.Core.Domain.Royalty;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Royalty;
using VTBlog.Core.SeedWorks;

namespace VTBlog.Core.Repositories
{
    public interface ITransactionRepository: IRepository<Transaction,Guid>
    {
        Task<PagedResult<TransactionDto>> GetAllPaging(string? userName, int fromMonth, int fromYear, int toMonth, int toYear,
            int pageIndex =1, int pageSize = 10);
    }
}

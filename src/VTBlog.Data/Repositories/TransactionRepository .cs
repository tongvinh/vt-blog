using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTBlog.Core.Domain.Royalty;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Royalty;
using VTBlog.Core.Repositories;
using VTBlog.Data.SeedWorks;

namespace VTBlog.Data.Repositories
{
    public class TransactionRepository(VTBlogContext context, IMapper mapper) : RepositoryBase<Transaction, Guid>(context), ITransactionRepository
    {
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResult<TransactionDto>> GetAllPaging(string? userName, int fromMonth, int fromYear, int toMonth, int toYear, int pageIndex = 1, int pageSize = 10)
        {
            var query = context.Transaction.AsQueryable();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                query = query.Where(x => x.ToUserName.Contains(userName));
            }
            if (fromMonth > 0 & fromYear > 0)
            {
                query = query.Where(x => x.DateCreated.Date.Month >= fromMonth & x.DateCreated.Year >= fromYear);
            }

            if (toMonth >0 & toYear >0)
            {
                query = query.Where(x => x.DateCreated.Date.Month <= toMonth & x.DateCreated.Year <= toYear);
            }

            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            return new PagedResult<TransactionDto>
            {
                Results = await _mapper.ProjectTo<TransactionDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }
    }
}

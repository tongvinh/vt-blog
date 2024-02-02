using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBlog.Core.Domain.Royalty;

namespace VTBlog.Core.Services
{
    public interface IRoyaltyService
    {
        Task<List<RoyaltyReportByUserDto>> GetRoyaltyReportByUserAsync(string? userName, int fromMonth, int fromYear, int toMonth, int toYear);

        Task<List<RoyaltyReportByMonthDto>> GetRoyaltyReportByMonthAsync(string? userName, int fromMonth, int fromYear,
            int toMonth, int toYear);
        Task PayRoyaltyForUserAsync(Guid fromUserId, Guid toUserId);
    }
}

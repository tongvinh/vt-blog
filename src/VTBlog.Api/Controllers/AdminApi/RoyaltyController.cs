using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VTBlog.Api.Extensions;
using VTBlog.Core.Domain.Royalty;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Royalty;
using VTBlog.Core.SeedWorks;
using VTBlog.Core.SeedWorks.Constants;
using VTBlog.Core.Services;

namespace VTBlog.Api.Controllers.AdminApi
{
    [Route("api/admin/royalty")]
    [ApiController]
    public class RoyaltyController(IUnitOfWork unitOfWork, IRoyaltyService royaltyService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRoyaltyService _royaltyService = royaltyService;

        [HttpGet]
        [Route("transaction-histories")]
        [Authorize(Permissions.Royalty.View)]
        public async Task<ActionResult<PagedResult<TransactionDto>>> GetTransactionHistory(string? keyword,
            int fromMonth, int fromYear, int toMonth, int toYear, int pageIndex, int pageSize = 10)
        {
            var result = await _unitOfWork.Transaction.GetAllPaging(keyword, fromMonth, fromYear, toMonth, toYear,
                pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Route("Royalty-report-by-user")]
        [Authorize(Permissions.Royalty.View)]
        public async Task<ActionResult<List<RoyaltyReportByUserDto>>> GetRoyaltyReportByUser(string? userName,
            int fromMonth, int fromYear, int toMonth, int toYear)
        {
            var result =
                await _royaltyService.GetRoyaltyReportByUserAsync(userName, fromMonth, fromYear, toMonth, toYear);
            return Ok(result);
        }

        [HttpGet]
        [Route("Royalty-report-by-month")]
        [Authorize(Permissions.Royalty.View)]
        public async Task<ActionResult<List<RoyaltyReportByMonthDto>>> GetRoyaltyReportByMonth(string? userName, int fromMonth,
            int fromYear, int toMonth, int toYear)
        {
            var result =
                await _royaltyService.GetRoyaltyReportByMonthAsync(userName, fromMonth, fromYear, toMonth, toYear);
            return Ok(result);
        }

        [HttpPost]
        [Route("{userId}")]
        [Authorize(Permissions.Royalty.Pay)]
        public async Task<IActionResult> PayRoyalty(Guid userId)
        {
            var fromUserId = User.GetUserId();
            await _royaltyService.PayRoyaltyForUserAsync(fromUserId, userId);
            return Ok();
        }
    }
}

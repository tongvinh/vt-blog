using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;
using VTBlog.Core.SeedWorks;
using VTBlog.Core.SeedWorks.Constants;

namespace VTBlog.Api.Controllers.AdminApi
{
    [Route("api/admin/series")]
    public class SeriesController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [Authorize(Permissions.Series.Create)]
        public async Task<IActionResult> CreateSeries([FromBody] CreateUpdateSeriesRequest request)
        {
            var post = _mapper.Map<CreateUpdateSeriesRequest, Series>(request);

            _unitOfWork.Series.Add(post);

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpPut]
        [Authorize(Permissions.Series.Edit)]
        public async Task<IActionResult> UpdateSeries(Guid id, [FromBody] CreateUpdateSeriesRequest request)
        {
            var post = await _unitOfWork.Series.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _mapper.Map(request, post);

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [Route("post-series")]
        [HttpPut()]
        [Authorize(Permissions.Series.Edit)]
        public async Task<IActionResult> AddPostSeries([FromBody] AddPostSeriesRequest request)
        {
            var isExisted = await _unitOfWork.Series.IsPostInSeries(request.SeriesId, request.PostId);
            if (isExisted)
            {
                return BadRequest($"Bài viết này đã nằm trong loạt bài."); 
            }

            await _unitOfWork.Series.AddPostToSeries(request.SeriesId, request.PostId, request.SortOrder);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [Route("post-series")]
        [HttpDelete()]
        [Authorize(Permissions.Series.Edit)]
        public async Task<IActionResult> DeletePostSeries([FromBody] AddPostSeriesRequest request)
        {
            var isExisted = await _unitOfWork.Series.IsPostInSeries(request.SeriesId, request.PostId);
            if (!isExisted)
            {
                return NotFound();
            }

            await _unitOfWork.Series.RemovePostToSeries(request.SeriesId, request.PostId);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [Route("post-series/{seriesId}")]
        [HttpGet()]
        [Authorize(Permissions.Series.Edit)]
        public async Task<ActionResult<List<PostInListDto>>> GetPostsInSeries(Guid seriesId)
        {
            var posts = await _unitOfWork.Series.GetAllPostsInSeries(seriesId);
            return Ok(posts);
        }

        [HttpDelete]
        [Authorize(Permissions.Series.Delete)]
        public async Task<IActionResult> DeleteSeries([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var post = await _unitOfWork.Series.GetByIdAsync(id);
                if (post == null)
                {
                    return NotFound();
                }
                _unitOfWork.Series.Remove(post);
            }

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Permissions.Series.View)]
        public async Task<ActionResult<SeriesDto>> GetSeriesById(Guid id)
        {
            var post = await _unitOfWork.Series.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Permissions.Series.View)]
        public async Task<ActionResult<PagedResult<SeriesInListDto>>> GetSeriesPaging(string? keyword,
            int pageIndex, int pageSize = 10)
        {
            var result = await _unitOfWork.Series.GetAllPaging(keyword, pageIndex, pageSize);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Permissions.Series.View)]
        public async Task<ActionResult<List<SeriesInListDto>>> GetAllSeries()
        {
            var result = await _unitOfWork.Series.GetAllAsync();
            var series = _mapper.Map<List<SeriesInListDto>>(result);
            return Ok(series);
        }
    }
}

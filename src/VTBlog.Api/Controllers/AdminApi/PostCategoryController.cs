using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VTBlog.Core.Domain.Content;
using VTBlog.Core.Models;
using VTBlog.Core.Models.Content;
using VTBlog.Core.SeedWorks;
using VTBlog.Core.SeedWorks.Constants;
using static VTBlog.Core.SeedWorks.Constants.Permissions;

namespace VTBlog.Api.Controllers.AdminApi
{
    [Route("api/admin/post-category")]
    public class PostCategoryController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [Authorize(Permissions.PostCategories.View)]
        public async Task<IActionResult> CreatePostCategory([FromBody] CreateUpdatePostCategoryRequest request)
        {
            var post = _mapper.Map<CreateUpdatePostCategoryRequest, PostCategory>(request);
            
            _unitOfWork.PostCategories.Add(post);

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpPut]
        [Authorize(Permissions.PostCategories.Edit)]
        public async Task<IActionResult> UpdatePostCategory(Guid id, [FromBody] CreateUpdatePostCategoryRequest request)
        {
            var post = await _unitOfWork.PostCategories.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _mapper.Map(request, post);

            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize(PostCategories.Delete)]
        public async Task<IActionResult> DeletePostCategory([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var post = await _unitOfWork.PostCategories.GetByIdAsync(id);
                if (post == null)
                {
                    return NotFound();
                }
                _unitOfWork.PostCategories.Remove(post);
            }
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(PostCategories.View)]
        public async Task<ActionResult<PostCategoryDto>> GetPostCategoryById(Guid id)
        {
            var category = await _unitOfWork.PostCategories.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<PostCategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(PostCategories.View)]
        public async Task<ActionResult<PagedResult<PostCategoryDto>>> GetPostCategoriesPaging(string? keyword,
            int pageIndex, int pageSize = 10)
        {
            var result = await _unitOfWork.PostCategories.GetAllPaging(keyword, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(PostCategories.View)]
        public async Task<ActionResult<List<PostCategoryDto>>> GetPostCategories()
        {
            var query = await _unitOfWork.PostCategories.GetAllAsync();
            var model = _mapper.Map<List<PostCategoryDto>>(query);
            return Ok(model);
        }
    }
}

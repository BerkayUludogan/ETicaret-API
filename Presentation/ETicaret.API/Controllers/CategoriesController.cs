using ETicaret.API.Attributes;
using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Categories.Commands.CreateCategory;
using ETicaret.Application.Features.Categories.Commands.DeleteCategory;
using ETicaret.Application.Features.Categories.Commands.UpdateCategory;
using ETicaret.Application.Features.Categories.Queries.GetCategories;
using ETicaret.Application.Features.Categories.Queries.GetCategoryById;
using MediatR; 
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [JwtAuthorize(RoleNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetCategoriesQueryRequest());
            return Ok(response);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetCategoryByIdQueryRequest { Id = id });
            return Ok(response);
        }
        [JwtAuthorize(RoleNames.Admin)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            updateCategoryCommandRequest.Id = id;
            var response = await _mediator.Send(updateCategoryCommandRequest);
            return Ok(response);
        }
        [JwtAuthorize(RoleNames.Admin)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommandRequest
            {
                Id = id
            });

            return Ok(response);
        }
    }
}

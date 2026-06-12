using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Categories.Commands.CreateCategory;
using ETicaret.Application.Features.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = RoleNames.Admin)]
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
    }
}

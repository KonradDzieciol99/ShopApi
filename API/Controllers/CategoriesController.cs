using Core.Brands.Commands;
using Core.Categories.Commands;
using Core.Categories.Handlers;
using Core.Categories.Queries;
using Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryOfProductDto>> GetCategoryOfProducts()
        {
            return await _mediator.Send(new GetCategoryOfProductsQuery());
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult<CategoryOfProductDto>> CreateCategory([FromQuery] string categoryName)//int pobiera się z querystring a file z body
        {
            return await _mediator.Send(new CreateCategoryCommand(categoryName));
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }

    }
}

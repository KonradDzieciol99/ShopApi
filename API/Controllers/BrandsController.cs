using Core.Brands.Commands;
using Core.Brands.Handlers;
using Core.Brands.Queries;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IEnumerable<BrandOfProductDto>> GetBrandOfProducts()
        {
            return await _mediator.Send(new GetBrandOfProductsQuery());
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult<BrandOfProductDto>> CreateBrand([FromQuery] string brandName)//int pobiera się z querystring a file z body
        {
            return await _mediator.Send(new CreateBrandCommand(brandName));
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _mediator.Send(new DeleteBrandCommand(id));
            return NoContent();
        }
    }
}

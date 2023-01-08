using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Models;
using Core.Products.Commands;
using Core.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("Paged")]
        public async Task<PagedList<ProductDto>> GetPagedProducts([FromQuery] ProductParamsRequest productParamsRequest)
        {
            return await _mediator.Send(new GetPagedProductsQuery(productParamsRequest));
        }

        [HttpGet("Discounted/Paged")]
        public async Task<PagedList<ProductDto>> GetDiscountedPagedProducts([FromQuery] ProductParamsRequest productParamsRequest)
        {
            return await _mediator.Send(new GetDiscountedPagedProductsQuery(productParamsRequest));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            return await _mediator.Send(new GetProductQuery(id));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ProductDto> CreateProduct([FromForm] CreateProductDto createProductDto)
        {
           return await _mediator.Send(new CreateProductCommand(createProductDto));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            await _mediator.Send(new UpdateProductCommand(productDto));
            return NoContent();
        }

    }
}

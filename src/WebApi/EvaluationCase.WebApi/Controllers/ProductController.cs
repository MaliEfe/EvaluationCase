using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using EvaluationCase.Application.Features.Commands.UpdateProduct;
using EvaluationCase.Application.Features.Queries.GetAllProducts;
using EvaluationCase.Application.Features.Queries.GetProductById;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllProductsQuery();
            return Ok(await mediator.Send(query));
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductByIdQuery() { Id = id };
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}

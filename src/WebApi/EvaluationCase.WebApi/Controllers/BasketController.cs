using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using EvaluationCase.Application.Features.Queries.GetAllBaskets;
using EvaluationCase.Application.Features.Commands.AddBasket;

namespace EvaluationCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator mediator;

        public BasketController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllBasketsQuery();
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddBasketCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}

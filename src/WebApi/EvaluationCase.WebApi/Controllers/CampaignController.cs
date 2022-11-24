using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Features.Queries.GetAllCampaigns;
using EvaluationCase.Application.Features.Commands.UpdateCampaign;
using EvaluationCase.Application.Features.Commands.SetInActiveCampaign;
using EvaluationCase.Application.Features.Queries.EvaluateCampaigns;
using EvaluationCase.Application.Features.Queries.GetCampaignById;

namespace EvaluationCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator mediator;

        public CampaignController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllCampaignsQuery();
            return Ok(await mediator.Send(query));
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCampaignByIdQuery() { Id = id };
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCampaignCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCampaignCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("set-inactive-campaign")]
        public async Task<IActionResult> SetInactiveCampaign(SetInActiveCampaignCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("campaign-evaluate")]
        public async Task<IActionResult> CampaignEvaluate(EvaluateCampaignQuery command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}

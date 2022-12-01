using MongoDB.Driver;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using EvaluationCase.Domain.Entities;
using EvaluationCase.Persistence.Context;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCase.Persistence.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(IMongoDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Campaign> SetInactiveCampaignAsync(Guid id)
        {
            var campaign = await GetByIdAsync(id);
            campaign.IsActive = false;

            return await UpdateAsync(campaign);
        }

    }
}

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
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IBasketRepository basketRepository;
        private readonly IRedisClient _redisCache;


        public CampaignRepository(ApplicationDbContext dbContext, IBasketRepository basketRepository, IRedisClient redisCache)
        {
            this.basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _dbContext = dbContext;
            _redisCache = redisCache;
        }

        public async Task<Campaign> CreateAsync(Campaign campaign)
        {
            campaign.CreateDate = DateTime.UtcNow;
            await _dbContext.Campaigns.InsertOneAsync(campaign);

            return campaign;
        }

        public async Task<List<Campaign>> GetAllAsync() =>
            await _dbContext.Campaigns.Find(Builders<Campaign>.Filter.Empty).ToListAsync();

        public async Task<Campaign> GetByIdAsync(Guid id) =>
            await _dbContext.Campaigns.Find(Builders<Campaign>.Filter.Eq(p => p.Id, id)).FirstOrDefaultAsync();

        public async Task<Campaign> UpdateAsync(Campaign entity)
        {
            return await _dbContext.Campaigns.FindOneAndReplaceAsync<Campaign>(Builders<Campaign>.Filter.Eq(c => c.Id, entity.Id), entity);
        }

        public async Task<Campaign> SetInactiveCampaignAsync(Guid id)
        {
            var campaign = await GetByIdAsync(id);
            campaign.IsActive = false;

            return await UpdateAsync(campaign);
        }

    }
}

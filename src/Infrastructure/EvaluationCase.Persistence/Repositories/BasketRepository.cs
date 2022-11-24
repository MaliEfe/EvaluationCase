using MongoDB.Driver;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Domain.Entities;
using EvaluationCase.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCase.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BasketRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Basket> CreateAsync(Basket basket)
        {
            basket.CreateDate = DateTime.UtcNow;
            await _dbContext.Baskets.InsertOneAsync(basket);

            return basket;
        }

        public async Task<List<Basket>> GetAllAsync() =>
            await _dbContext.Baskets.Find(Builders<Basket>.Filter.Empty).ToListAsync();

        public async Task<Basket> GetByIdAsync(Guid id) =>
            await _dbContext.Baskets.Find(Builders<Basket>.Filter.Eq(p => p.Id, id)).FirstOrDefaultAsync();

        public async Task<Basket> UpdateAsync(Basket entity)
        {
            return await _dbContext.Baskets.FindOneAndReplaceAsync<Basket>(Builders<Basket>.Filter.Eq(c => c.Id, entity.Id), entity);
        }
    }
}

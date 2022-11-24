using Microsoft.EntityFrameworkCore;
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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.CreateDate = DateTime.UtcNow;
            await _dbContext.Products.InsertOneAsync(product);

            return product;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _dbContext.Products.Find(Builders<Product>.Filter.Empty).ToListAsync();

        public async Task<Product> GetByIdAsync(Guid id) =>
            await _dbContext.Products.Find(Builders<Product>.Filter.Eq(p => p.Id, id)).FirstOrDefaultAsync();

        public async Task<Product> UpdateAsync(Product entity)
        {
            return await _dbContext.Products.FindOneAndReplaceAsync<Product>(Builders<Product>.Filter.Eq(c => c.Id, entity.Id), entity);
        }
    }
}

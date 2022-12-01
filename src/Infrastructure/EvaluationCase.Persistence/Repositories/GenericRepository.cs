using Microsoft.EntityFrameworkCore;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Domain.Common;
using EvaluationCase.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using EvaluationCase.Domain.Entities;
using SharpCompress.Common;

namespace EvaluationCase.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<T> _dbCollection;

        public GenericRepository(IMongoDBContext dbContext)
        {
            _mongoContext = dbContext;
            _dbCollection = _mongoContext.GetCollection<T>(typeof(T).Name);
        }


        public async Task<T> CreateAsync(T entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            await _dbCollection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<List<T>> GetAllAsync() =>
            await _dbCollection.Find(Builders<T>.Filter.Empty).ToListAsync();


        public async Task<T> GetByIdAsync(Guid id) =>
            await _dbCollection.Find(Builders<T>.Filter.Eq(p => p.Id, id)).FirstOrDefaultAsync();


        public async Task<T> UpdateAsync(T entity) =>
            await _dbCollection.FindOneAndReplaceAsync<T>(Builders<T>.Filter.Eq(c => c.Id, entity.Id), entity);
    }
}

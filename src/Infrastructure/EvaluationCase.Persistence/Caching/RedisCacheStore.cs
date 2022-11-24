using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Interfaces.Repositories;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCase.Persistence.Caching
{
    public class RedisCacheStore : ICacheStore
    {
        private readonly IRedisClient _redisCache;
        public RedisCacheStore(IRedisClient redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task AddAsync<T>(T data, string cacheKey, TimeSpan expirationTime)
        {
            await _redisCache.Db0.AddAsync(cacheKey, data, expirationTime);

        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await _redisCache.Db0.GetAsync<T>(key);
        }

        public async Task RemoveAsync(string[] keys)
        {
            await _redisCache.Db0.RemoveAllAsync(keys);
        }
    }
}

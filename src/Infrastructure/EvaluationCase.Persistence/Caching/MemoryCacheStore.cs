using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using EvaluationCase.Application.Interfaces.Repositories;

namespace EvaluationCase.Persistence.Caching
{
    public class MemoryCacheStore : ICacheStore
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<string, TimeSpan> _expirationConfiguration;

        public MemoryCacheStore(
            IMemoryCache memoryCache,
            Dictionary<string, TimeSpan> expirationConfiguration)
        {
            _memoryCache = memoryCache;
            this._expirationConfiguration = expirationConfiguration;
        }

        public Task AddAsync<T>(T request, string cacheKey, TimeSpan expirationTime)
        {
            _memoryCache.Set(cacheKey, request, expirationTime);

            return Task.CompletedTask;
        }

        public Task<T> GetAsync<T>(string key)
        {
            var result = _memoryCache.Get<T>(key);

            return Task.FromResult((T)result);
        }

        public Task RemoveAsync(string[] keys)
        {
            _memoryCache.Remove(keys);

            return Task.CompletedTask;
        }
    }
}
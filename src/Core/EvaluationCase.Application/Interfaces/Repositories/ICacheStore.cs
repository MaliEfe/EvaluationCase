using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using EvaluationCase.Domain.Common;
using EvaluationCase.Application.Dtos;

namespace EvaluationCase.Application.Interfaces.Repositories
{
    public interface ICacheStore
    {

        Task AddAsync<T>(T request, string cacheKey, TimeSpan expirationTime);

        Task<T> GetAsync<T>(string key);

        Task RemoveAsync(string[] keys);

    }
}
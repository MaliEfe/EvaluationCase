using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Services;
using EvaluationCase.Persistence.Caching;
using EvaluationCase.Persistence.Context;
using EvaluationCase.Persistence.Repositories;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<CampaignEvaluateService>();
            services.AddScoped<ICacheStore, RedisCacheStore>();

        }
        public static void AddRedis(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            var conf = new RedisConfiguration
            {
                ConnectionString = configuration.GetConnectionString("Redis"),
                ServerEnumerationStrategy = new ServerEnumerationStrategy
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                }
            };

            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(conf);
        }
    }
}

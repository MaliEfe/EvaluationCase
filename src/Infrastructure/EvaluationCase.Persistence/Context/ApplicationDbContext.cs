using DnsClient.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using EvaluationCase.Domain.Entities;
using EvaluationCase.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace EvaluationCase.Persistence.Context
{
    public class ApplicationDbContext
    {
        public IMongoCollection<Campaign> Campaigns { get; set; }
        public IMongoCollection<Product> Products { get; set; }
        public IMongoCollection<Basket> Baskets { get; set; }

        public ApplicationDbContext(IOptions<DatabaseSetting> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);

            Products = database.GetCollection<Product>("products");
            Campaigns = database.GetCollection<Campaign>("campaigns");
            Baskets = database.GetCollection<Basket>("baskets");
        }
    }
}

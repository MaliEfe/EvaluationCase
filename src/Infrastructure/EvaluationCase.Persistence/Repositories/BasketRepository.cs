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
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(IMongoDBContext dbContext) : base(dbContext)
        {
        }
    }
}

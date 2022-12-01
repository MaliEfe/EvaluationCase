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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDBContext dbContext) : base(dbContext)
        {
        }
    }
}

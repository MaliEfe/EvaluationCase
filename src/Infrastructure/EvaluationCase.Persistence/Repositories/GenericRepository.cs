﻿using Microsoft.EntityFrameworkCore;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Domain.Common;
using EvaluationCase.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCase.Persistence.Repositories
{
    public class GenericRepository<T> //: IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public async Task<T> AddAsync(T entity)
        //{
        //    await dbContext.Set<T>().AddAsync(entity);
        //    await dbContext.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task<List<T>> GetAll()
        //{
        //    return await dbContext.Set<T>().ToListAsync();
        //}

        //public async Task<T> GetById(Guid id)
        //{
        //    return await dbContext.Set<T>().FindAsync(id);
        //}
    }
}

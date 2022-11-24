using EvaluationCase.Application.Dtos;
using EvaluationCase.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCase.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}

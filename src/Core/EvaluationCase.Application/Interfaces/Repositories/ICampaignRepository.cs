using EvaluationCase.Application.Dtos;
using EvaluationCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCase.Application.Interfaces.Repositories
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        Task<Campaign> SetInactiveCampaignAsync(Guid id);
    }
}

using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Contracts.Persistence;

public interface IRateRepository: IRepository<CoinRate> 
{
    Task<bool> SaveRate(CoinRate response);
}
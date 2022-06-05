using Revix.Rate.Application.Contracts;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Infrastructure.Repository;

public class RateRepository : BaseRepository<CoinRate>,IRateRepository {
    public RateRepository(IDataContext<CoinRate> context) : base(context)
    {
        
    }

    public async Task<bool> SaveRate(CoinRate response)
    {
        await Create(response);
        return true;
    }
}
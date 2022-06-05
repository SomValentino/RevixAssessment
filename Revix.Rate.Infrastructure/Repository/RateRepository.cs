using Revix.Rate.Application.Contracts;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Infrastructure.Repository;

public class RateRepository : BaseRepository<ApiResponse>,IRateRepository {
    public RateRepository(IDataContext<ApiResponse> context) : base(context)
    {
        
    }

    public async Task<bool> SaveRate(ApiResponse response)
    {
        await Create(response);
        return true;
    }
}
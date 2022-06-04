using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Contracts.Persistence;

public interface IRateRepository: IRepository<ApiResponse> 
{
    Task<bool> SaveRate(ApiResponse response);
}
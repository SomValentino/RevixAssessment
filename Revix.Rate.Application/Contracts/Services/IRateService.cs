using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Contracts.Services;

public interface IRateService 
{
    Task<CoinRate> GetDailyRate(string path);

    Task<IEnumerable<CoinRate>> GetRateForDateRange(DateTime startDate, DateTime endDate);

    Task SaveRate(CoinRate response);
}
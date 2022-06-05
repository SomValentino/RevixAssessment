using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Services;

public interface IRateService 
{
    Task GetDailyRate(string path);

    Task<IEnumerable<ApiResponse>> GetRateForDateRange(DateTime startDate, DateTime endDate);

    Task SaveRate(ApiResponse response);
}
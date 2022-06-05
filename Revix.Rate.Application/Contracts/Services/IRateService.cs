using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Services;

public interface IRateService {
    
    Task<ApiResponse> GetDailyRate(string path);

    Task SaveRate(ApiResponse response);
}
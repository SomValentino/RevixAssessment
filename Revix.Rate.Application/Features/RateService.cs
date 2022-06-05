using MongoDB.Driver;
using Newtonsoft.Json;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Application.Services;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Features;

public class RateService : IRateService {
    private readonly IRateRepository _repository;
    private readonly HttpClient _client;

    public RateService (IRateRepository repository, IHttpClientFactory httpFactory) {
        _repository = repository;
        _client = httpFactory.CreateClient ("RateClient");
    }

    public async Task<ApiResponse> GetDailyRate (string path) {
        var response = await _client.GetAsync (path);

        response.EnsureSuccessStatusCode ();

        var data = await response.Content.ReadAsStringAsync ();

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse> (data);

        await SaveRate(apiResponse);

        return apiResponse;
    }

    public async Task SaveRate (ApiResponse response) 
    {
        var filter = Builders<ApiResponse>.Filter.Eq (x => x.Status.Timestamp.Date, response.Status.Timestamp.Date);

        var existResponse = (await _repository.GetAsync (filter)).SingleOrDefault();

        if (existResponse == null) {
            await _repository.Create (response);
            return;
        }

        existResponse.Data = response.Data;
        existResponse.Status = response.Status;

        await _repository.Update (existResponse);

    }
}
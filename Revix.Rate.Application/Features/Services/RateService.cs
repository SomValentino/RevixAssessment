using MongoDB.Driver;
using Newtonsoft.Json;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Application.Contracts.Services;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Features.Services;

public class RateService : IRateService {
    private readonly IRateRepository _repository;
    private readonly HttpClient _client;

    public RateService (IRateRepository repository, IHttpClientFactory httpFactory) {
        _repository = repository;
        _client = httpFactory.CreateClient ("RateClient");
    }

    public async Task<CoinRate> GetDailyRate (string path) {
        // make api call
        var response = await _client.GetAsync (path);
        // ensure success
        response.EnsureSuccessStatusCode ();

        var data = await response.Content.ReadAsStringAsync ();

        var coinRate = JsonConvert.DeserializeObject<CoinRate> (data);

        return coinRate;
    }

    public async Task<IEnumerable<CoinRate>> GetRateForDateRange (DateTime startDate, DateTime endDate) {
        // create date range filter
        var startDateFilter = Builders<CoinRate>.Filter.Gte (x => x.Status.Timestamp, startDate);
        var endDateFilter = Builders<CoinRate>.Filter.Lte (x => x.Status.Timestamp, endDate);
        var combinedFilter = Builders<CoinRate>.Filter.And (startDateFilter, endDateFilter);
        // get rates that fall between the date range
        var rates = await _repository.GetAsync (combinedFilter);

        return rates;
    }

    public async Task SaveRate (CoinRate response) {
        // build filter for today's rate
        var filter = Builders<CoinRate>.Filter;
        var date = DateTime.Now.Date;
        var combinedfilter = filter.And (
            filter.Gte (x => x.Status.Timestamp, date),
            filter.Lt (x => x.Status.Timestamp, date.AddDays (1))
        );
        // check if any rate have stored for today
        var existResponse = (await _repository.GetAsync (combinedfilter)).SingleOrDefault ();
        // if not add new rate to db
        if (existResponse == null) {
            await _repository.Create (response);
            return;
        }
        // if so update existing rate
        existResponse.Data = response.Data;
        existResponse.Status = response.Status;

        await _repository.Update (existResponse);

    }
}
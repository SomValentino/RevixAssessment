using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revix.Rate.Application.Contracts;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Domain.Models;
using Revix.Rate.Infrastructure.Data;
using Revix.Rate.Infrastructure.Repository;

namespace Revix.Rate.Infrastructure;

public static class ApplicationInfrastructureServices {
    public static IServiceCollection AddApplicationInfrastructureService (this IServiceCollection serviceCollection, IConfiguration configuration) {
        serviceCollection.AddScoped<IDataContext<ApiResponse>, RateDataContext> (options => {
            var connectionString = configuration["connectionString"];
            var database = configuration["database"];
            var collectionName = configuration["collectionName"];

            return new RateDataContext (connectionString, database, collectionName);
        });

        serviceCollection.AddScoped<IRateRepository, RateRepository>();
        return serviceCollection;
    }
}
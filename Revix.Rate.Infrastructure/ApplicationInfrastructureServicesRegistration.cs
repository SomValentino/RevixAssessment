using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revix.Rate.Application.Contracts;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Domain.Models;
using Revix.Rate.Infrastructure.Data;
using Revix.Rate.Infrastructure.Repository;

namespace Revix.Rate.Infrastructure;

public static class ApplicationInfrastructureServicesRegistration 
{
    public static IServiceCollection AddApplicationInfrastructureService (this IServiceCollection serviceCollection, IConfiguration configuration) 
    {
        serviceCollection.AddScoped<IDataContext<CoinRate>, RateDataContext> (options => {
            var connectionString = configuration["DatabaseSettings:ConnectionString"];
            var database = configuration["DatabaseSettings:DatabaseName"];
            var collectionName = configuration["DatabaseSettings:CollectionName"];

            return new RateDataContext (connectionString, database, collectionName);
        });

        serviceCollection.AddScoped<IRateRepository, RateRepository>();
        return serviceCollection;
    }
}
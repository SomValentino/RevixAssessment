using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revix.Rate.Application.Features.Services;
using Revix.Rate.Application.Contracts.Services;

namespace Revix.Rate.Application;

public static class ApplicationServiceRegistration {
   public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
   {
       serviceCollection.AddScoped<IRateService,RateService>();

       serviceCollection.AddHttpClient("RateClient",options => {
           options.BaseAddress = new Uri(configuration["ExternalProviderSettings:BaseUrl"]);
           options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           options.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration["ExternalProviderSettings:ApiKey"]);
       });

        serviceCollection.AddHostedService<RateHostedService>();

        return serviceCollection;
   }
}
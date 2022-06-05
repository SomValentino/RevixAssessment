using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revix.Rate.Application.Features;
using Revix.Rate.Application.Services;

namespace Revix.Rate.Application;

public static class ApplicationServices {
   public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
   {
       serviceCollection.AddScoped<IRateService,RateService>();

       serviceCollection.AddHttpClient("RateClient",options => {
           options.BaseAddress = new Uri(configuration["baseUrl"]);
           options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
       });
       
       return serviceCollection;
   }
}
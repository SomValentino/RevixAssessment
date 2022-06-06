using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Revix.Rate.Application.Contracts.Services;

namespace Revix.Rate.Application.Features.Services {
    public class RateHostedService : BackgroundService {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RateHostedService> _logger;

        public RateHostedService (IServiceProvider serviceProvider, ILogger<RateHostedService> logger) {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                _logger.LogInformation ("Worker running at: {time}", DateTimeOffset.Now);
                using var scope = _serviceProvider.CreateScope ();
                var rateService = scope.ServiceProvider.GetRequiredService<IRateService> ();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration> ();
                var path = configuration["ExternalProviderSettings:RatePath"];
                try {
                    _logger.LogInformation ("Getting daily rate at: {time}", DateTimeOffset.Now);
                    var coinRate = await rateService.GetDailyRate (path);
                    await rateService.SaveRate(coinRate);
                    _logger.LogInformation ("Succesfully obtained daily rate at: {time}", DateTimeOffset.Now);
                } catch (Exception ex) {
                    _logger.LogError (ex.Message, ex);
                    throw new ApplicationException(ex.Message, ex);
                }
                await Task.Delay (600000, stoppingToken);
            }
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revix.Rate.Application.Services;
using Revix.Rate.Domain.Models;
using System.Collections.Generic;

namespace Revix.Rate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public ILogger<RateController> _logger { get; }

        public RateController(IRateService rateService, ILogger<RateController> logger)
        {
            _rateService = rateService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRates(DateTime? startDate = null, DateTime? endDate = null)
        {
            var sDate = startDate ?? DateTime.Now;
            var eDate = endDate ?? DateTime.Now;

            if (sDate > eDate)
                return BadRequest("Wrong dates supplied. endDate must greater or equal to startDate");

            var rates = await _rateService.GetRateForDateRange(sDate, eDate);
            
            return Ok(rates);
        }
    }
}

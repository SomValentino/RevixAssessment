using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revix.Rate.Application.Contracts.Services;
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
            // check for nulls if so make date range shoul be 1 day
            var sDate = startDate ?? DateTime.Now.AddDays(-1);
            var eDate = endDate ?? DateTime.Now;
            // check if date range is invalid
            if (sDate > eDate)
                return BadRequest("Wrong dates supplied. endDate must greater or equal to startDate");
            // get rates based on the date range
            var rates = await _rateService.GetRateForDateRange(sDate, eDate);
            
            return Ok(rates);
        }
    }
}

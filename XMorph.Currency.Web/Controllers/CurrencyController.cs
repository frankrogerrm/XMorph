using XMorph.Currency.Core.Utilities;

namespace XMorph.Currency.Web.Controllers {
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using XMorph.Currency.Core.Models;
    using XMorph.Currency.Core.Services;

    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase {


        private readonly ILogger<CurrencyController> _logger;
        private ICompanyRateService _companyRateService;

        public CurrencyController(ILogger<CurrencyController> logger,
                                    ICompanyRateService companyRateService) {
            _logger = logger;
            _companyRateService = companyRateService;
        }

        [HttpGet(Name = "GetCurrency")]
        public IActionResult Get() {
            var result = _companyRateService.GetAllCompanyRates().Format();
            return Ok(result);
        }
    }
}

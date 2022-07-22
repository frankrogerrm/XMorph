using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XMorph.Currency.Core.Services;
using XMorph.Currency.Core.Utilities;

namespace XMorph.Currency.Web.Controllers {

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CurrencyController : ControllerBase {

        private readonly ICompanyRateService _companyRateService;

        public CurrencyController(  ICompanyRateService companyRateService) {

            _companyRateService = companyRateService;

        }

        [HttpGet]
        public IActionResult Get() {
            var result = _companyRateService.GetAllCompanyRates().BeautyJson();
            return Ok(result);
        }
    }
}

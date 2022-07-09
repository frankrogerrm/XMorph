namespace XMorph.Currency.Web.Controllers.Api {
        
    using Microsoft.AspNetCore.Mvc;
    using XMorph.Currency.Core.Enums;
    using XMorph.Currency.Core.Services;

    [Route("api/Currency")]
    [ApiController]
    public class CurrencyApiController : ControllerBase {

        private ICurrencyService _currencyService;
        private ICompanyRateService _companyRateService;

        public CurrencyApiController(   ICurrencyService currencyService,
                                        ICompanyRateService companyRateService) {

            _companyRateService=companyRateService;
            _currencyService = currencyService;
        }

        [HttpGet]
        [Route("Securex")]
        public IActionResult GetSecurexData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.SECUREX);
            return Ok(result);
        }

        [HttpGet]
        [Route("Tkambio")]
        public IActionResult GetTkambioData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.TKAMBIO);
            return Ok(result);
        }

        [HttpGet]
        [Route("Kambista")]
        public IActionResult GetKambistaData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.KAMBISTA);
            return Ok(result);
        }

        [HttpGet]
        [Route("CambioSeguro")]
        public IActionResult GetCambioSeguroData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.CAMBIOSEGURO);
            return Ok(result);
        }

        [HttpGet]
        [Route("Rextie")]
        public IActionResult GetRextieData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.REXTIE);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get() {
            var result = _companyRateService.GetAllCompanyRates();
            return Ok(result);
        }

        [HttpGet]
        [Route("UpdateAllRates")]
        public async Task<IActionResult> UpdateAllRates() {
            var result = await _currencyService.UpdateAllRates();
            return Ok(result);
        }

        [HttpGet]
        [Route("CleanCompanyRateByDays/{days}")]
        public IActionResult CleanCompanyRateByDays(int days) {
            var result = _companyRateService.CleanCompanyRateByDays(days);
            return Ok(result);
        }

        [HttpGet]
        [Route("ForceCleanCompanyRateByDays")]
        public IActionResult ForceCleanCompanyRateByDays() {
            var result = _companyRateService.ForceCleanCompanyRateByDays();
            return Ok(result);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XMorph.Currency.Core.Enums;
using XMorph.Currency.Core.Services;
using XMorph.Currency.Core.Utilities;

namespace XMorph.Currency.Web.Controllers.Api {
    
    [Route("api/Currency")]
    [ApiController]
    [Authorize]
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
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.SECUREX).BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        [Route("Tkambio")]
        public IActionResult GetTkambioData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.TKAMBIO).BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        [Route("Kambista")]
        public IActionResult GetKambistaData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.KAMBISTA).BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        [Route("CambioSeguro")]
        public IActionResult GetCambioSeguroData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.CAMBIOSEGURO).BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        [Route("Rextie")]
        public IActionResult GetRextieData() {
            var result = _companyRateService.GetCompanyRateByCompanyId((int)CompanyNameEnum.REXTIE).BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get() {
            var result = _companyRateService.GetAllCompanyRates().BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        [Route("UpdateAllRates")]
        public async Task<IActionResult> UpdateAllRates() {
            var result = (await _currencyService.UpdateAllRates()).BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        [Route("CleanCompanyRateByDays/{days}")]
        public IActionResult CleanCompanyRateByDays(int days) {
            var result = _companyRateService.CleanCompanyRateByDays(days).BeautyJson();
            return Ok(result);
        }

        [HttpGet]
        [Route("ForceCleanCompanyRateByDays")]
        public IActionResult ForceCleanCompanyRateByDays() {
            var result = _companyRateService.ForceCleanCompanyRateByDays().BeautyJson();
            return Ok(result);
        }

    }
}

using Newtonsoft.Json;
using XMorph.Currency.Core.Enums;
using XMorph.Currency.Core.Models;
using XMorph.Currency.Core.Utilities;

namespace XMorph.Currency.Core.Services {

    public interface ICurrencyService {
        //Task<CompanyRateModel> WebToString(string companyName);
        Task<List<CompanyRateModel>> WebToStringAll();
        Task<List<CompanyRateModel>> UpdateAllRates();

    }

    public class CurrencyService : ICurrencyService {

        private ICompanyService _companyService;
        private ICompanyRateService _companyRateService;

        public CurrencyService(ICompanyService companyService,
                                ICompanyRateService companyRateService) {

            _companyService = companyService;
            _companyRateService = companyRateService;

        }

        public async Task<List<CompanyRateModel>> UpdateAllRates() {

            var result = new List<CompanyRateModel>();
            var companiesResult = await WebToStringAll();
            foreach (var item in companiesResult) {

                result.Add(_companyRateService.SaveUpdateCompanyRate(item));

            }

            return result;

        }

        public async Task<List<CompanyRateModel>> WebToStringAll() {

            var result = new List<CompanyRateModel>();

            var securexModel = _companyService.GetCompanyById((int)CompanyNameEnum.SECUREX);
            var tkambioModel = _companyService.GetCompanyById((int)CompanyNameEnum.TKAMBIO);
            var kambistaModel = _companyService.GetCompanyById((int)CompanyNameEnum.KAMBISTA);
            var cambioSeguroModel = _companyService.GetCompanyById((int)CompanyNameEnum.CAMBIOSEGURO);
            var rextieModel = _companyService.GetCompanyById((int)CompanyNameEnum.REXTIE);

            result.Add(WebToModel(securexModel));
            result.Add(await TkambioCurrencyStringToModel(tkambioModel));
            result.Add(WebToModel(kambistaModel));
            result.Add(WebToModel(cambioSeguroModel));
            result.Add(await RextieCurrencyStringToModel(rextieModel));

            return result;

        }

        private CompanyRateModel WebToModel(CompanyModel company) {

            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = client.GetAsync(company.Url).Result;
            using HttpContent content = response.Content;
            string sourcePageString = content.ReadAsStringAsync().Result.CleanString();

            var filterSellKey = company.CompanyFilterModels.FirstOrDefault(x => x.CompanyFilterType.Type.ToUpper().Equals(CompanyFilterTypeEnum.SELL_RATE.ToUpper()));
            var filterBuyKey = company.CompanyFilterModels.FirstOrDefault(x => x.CompanyFilterType.Type.ToUpper().Equals(CompanyFilterTypeEnum.BUY_RATE.ToUpper()));


            string sellingrate = GetStringBetween(sourcePageString, filterSellKey.KeyFilter.CleanString());
            string buyingrate = GetStringBetween(sourcePageString, filterBuyKey.KeyFilter.CleanString());

            return new CompanyRateModel() {

                SellRate = Convert.ToDouble(sellingrate),
                BuyRate = Convert.ToDouble(buyingrate),
                CompanyId = company.Id,
                CreatedDate = DateTime.Now,
                Status = true,
                UpdatedDate = DateTime.Now
            };
        }

        private async Task<CompanyRateModel> TkambioCurrencyStringToModel(CompanyModel company) {

            var content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("action", "get_exchange_rate")
            });

            using var client = new HttpClient();
            var response = await client.PostAsync(company.Url, content);
            string rawResult = response.Content.ReadAsStringAsync().Result;
            var tkambioModel = JsonConvert.DeserializeObject<TkambioModelResult>(rawResult);

            return new CompanyRateModel {
                SellRate = tkambioModel.selling_rate,
                BuyRate = tkambioModel.buying_rate,
                CompanyId = company.Id,
                CreatedDate = DateTime.Now,
                Status = true,
                UpdatedDate = DateTime.Now
            };
        }
        private async Task<CompanyRateModel> RextieCurrencyStringToModel(CompanyModel company) {

            using var client = new HttpClient();
            var response = await client.PostAsync(company.Url, null);
            string rawResult = response.Content.ReadAsStringAsync().Result;
            var rextieModel = JsonConvert.DeserializeObject<RextieModelResult>(rawResult);

            return new CompanyRateModel {
                SellRate = Convert.ToDouble(rextieModel?.fx_rate_sell),
                BuyRate = Convert.ToDouble(rextieModel?.fx_rate_buy),
                CompanyId = company.Id,
                CreatedDate = DateTime.Now,
                Status = true,
                UpdatedDate = DateTime.Now
            };
        }

        private string GetStringBetween(string htmlString, string firstString) {
            int pos = htmlString.IndexOf(firstString, StringComparison.Ordinal) + firstString.Length;
            var stringBetween = htmlString.Substring(pos, 20).Replace(" ", string.Empty);
            string value = string.Empty;
            foreach (char item in stringBetween) {

                if (item.Equals('<')) {
                    break;
                }

                value += item;
            }

            return value.Trim();
        }

    }
}

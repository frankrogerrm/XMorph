namespace XMorph.Currency.Function {

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class XMorphCurrencyFunction {
        [FunctionName("UpdateAllRates")]
        public async Task UpdateAllRates([TimerTrigger("0 */15 12-23,0-4 * * *")] TimerInfo myTimer, ILogger log) {
            using var client = new HttpClient();
            var url = Environment.GetEnvironmentVariable("UPDATE_ALL_RATES_URL");
            await client.GetAsync(url);
            log.LogInformation($"C# Timer trigger function executed UpdateAllRates at: {DateTime.Now}");

        }

        [FunctionName("CleanCompanyRateByDays")]
        public async Task CleanCompanyRateByDays([TimerTrigger("0 5 0,12 * * 0-5")] TimerInfo myTimer, ILogger log) {
            using var client = new HttpClient();
            var url = Environment.GetEnvironmentVariable("CLEAN_COPMPANY_RATE_URL") + Environment.GetEnvironmentVariable("CLEAN_COPMPANY_RATE_DAYS");
            await client.GetAsync(url);
            log.LogInformation($"C# Timer trigger function executed CleanCompanyRateByDays at: {DateTime.Now}");

        }
    }
}

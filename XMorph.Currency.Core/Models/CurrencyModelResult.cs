using System;

namespace XMorph.Currency.Core.Models {

    public class CurrencyModelResult {
        public double SellRate { get; set; }
        public double BuyRate { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CompanyId { get; set; }

    }
}

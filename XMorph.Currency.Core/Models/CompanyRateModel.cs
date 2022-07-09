namespace XMorph.Currency.Core.Models {

    public class CompanyRateModel {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double SellRate { get; set; }
        public double BuyRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

namespace XMorph.Currency.Core.Models {

    public class CompanyModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<CompanyFilterModel> CompanyFilterModels { get; set; }
    }

    public class CompanyFilterModel {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string KeyFilter { get; set; }
        public int FilterTypeId { get; set; }
        public CompanyFilterTypeModel CompanyFilterType { get; set; }
    }

    public class CompanyFilterTypeModel {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}

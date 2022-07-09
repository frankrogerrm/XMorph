namespace XMorph.Currency.DAL.Entities {

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompanyFilterType")]
    public class CompanyFilterType {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}

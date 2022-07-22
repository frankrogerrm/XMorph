using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XMorph.Currency.DAL.Entities {

    [Table("CompanyFilter")]
    public class CompanyFilter {
        [Key]
        public int Id { get; set; }
        public int Companyid { get; set; }
        public string KeyFilter { get; set; }
        public int FilterTypeId { get; set; }
        public virtual CompanyFilterType FilterType { get; set; }
    }
}

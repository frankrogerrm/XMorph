using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XMorph.Currency.DAL.Entities {

    [Table("CompanyFilterType")]
    public class CompanyFilterType {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}

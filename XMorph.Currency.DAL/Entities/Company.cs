using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XMorph.Currency.DAL.Entities {

    [Table("Company")]
    public class Company {
        [Key]
        public int Id { get; set; }
        public string Name{ get; set; }
        public bool Status { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated{ get; set; }
        public DateTime DateUpdated { get; set; }
    }
}

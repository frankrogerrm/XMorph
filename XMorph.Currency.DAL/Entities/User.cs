using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XMorph.Currency.DAL.Entities {

    [Table("User")]
    public class User {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? Token { get; set; }
        public DateTime TokenCreatedDate { get; set; }
        public DateTime TokenExpiresDate { get; set; }
    }
}

namespace XMorph.Currency.Core.Models {
    public class UserModel {
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

    public class UserNoPasswordModel {
        public int Id { get; set; }
        public string Email { get; set; }
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

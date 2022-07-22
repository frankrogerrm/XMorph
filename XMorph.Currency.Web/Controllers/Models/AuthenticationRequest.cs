using System.ComponentModel.DataAnnotations;

namespace XMorph.Currency.Web.Controllers.Models {

    public class AuthenticationRequest {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}

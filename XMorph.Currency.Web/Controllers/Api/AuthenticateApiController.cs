using AgileObjects.AgileMapper;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using XMorph.Currency.Core.Models;
using XMorph.Currency.Core.Services;
using XMorph.Currency.Core.Utilities;
using XMorph.Currency.Web.Controllers.Models;

namespace XMorph.Currency.Web.Controllers.Api {


    [Route("api/Currency")]
    [ApiController]
    [Authorize]

    public class AuthenticateApiController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public AuthenticateApiController(IConfiguration configuration, IUserService userService) {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        public IActionResult Authenticate(AuthenticationRequest request) {

            var user = _userService.GetUserByUserName(request.Email);
            if (user == null) {
                return Ok("User does not exists");
            }

            var isValidPassword = VerifyHash(request.Password, user.PasswordSalt, user.PasswordHash);
            if (!isValidPassword) {
                return Ok("Wrong password");
            }
            user = Authenticate(user);
            return Ok(user.BeautyUserJson());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SignUp")]
        public IActionResult SignUp(AuthenticationRequest request) {

            var user = _userService.GetUserByUserName(request.Email);
            if (user != null) {
                return Ok("User already exists");
            }

            user = SaveUser(request);
            return Ok(user.BeautyUserJson());
        }

        private UserModel Authenticate(UserModel user) {

            var accessToken = GenerateJSONWebToken();
            user = _userService.UpdateToken(user.Id, accessToken);
            SetJWTCookie(accessToken);
            return user;

        }
                
        private UserModel SaveUser(AuthenticationRequest request) {
            var userModel = Mapper.Map(request).ToANew<UserModel>();
            userModel.PasswordSalt = GenerateSalt();
            userModel.PasswordHash = HashPassword(request.Password, userModel.PasswordSalt);
            var accessToken = GenerateJSONWebToken();
            userModel.Token = accessToken;
            var user = _userService.SignUp(userModel);
            SetJWTCookie(accessToken);
            return user;
        }

        private string GenerateJSONWebToken() {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddHours(int.Parse(_configuration["Jwt:ExpireHours"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetJWTCookie(string token) {
            var cookieOptions = new CookieOptions {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(int.Parse(_configuration["Jwt:ExpireHours"])),
            };
            Response.Cookies.Append("jwtCookie", token, cookieOptions);
        }

        private byte[] GenerateSalt() {
            var buffer = new byte[16];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(buffer);
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt) {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024;

            return argon2.GetBytes(16);
        }

        private bool VerifyHash(string password, byte[] salt, byte[] hash) {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
    }




}

using AgileObjects.AgileMapper;
using Microsoft.Extensions.Configuration;
using XMorph.Currency.Core.Models;
using XMorph.Currency.DAL.Entities;
using XMorph.Currency.Repository.Generic.Interface;

namespace XMorph.Currency.Core.Services {

    public interface IUserService {
        UserModel? GetUserById(int id);
        UserModel? GetUserByUserName(string email);
        UserModel? Login(string email, string password);
        bool UserExists(string email);
        UserModel SignUp(UserModel userModel);
        UserModel UpdateToken(int id, string token);
    }

    public class UserService : IUserService {

        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IGenericRepository<User> userRepository, IConfiguration configuration) {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public UserModel? GetUserById(int id) {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id.Equals(id) && x.IsEnabled && x.TokenExpiresDate >= DateTime.Now);
            return user == null ? null : Mapper.Map(user).ToANew<UserModel>();
        }

        public UserModel? GetUserByUserName(string email) {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Email.Equals(email) && x.IsEnabled && x.TokenExpiresDate >= DateTime.Now);
            return user == null ? null : Mapper.Map(user).ToANew<UserModel>();
        }

        public bool UserExists(string email) {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Email.Equals(email) && x.IsEnabled && x.TokenExpiresDate >= DateTime.Now);
            return user != null;
        }

        public UserModel SignUp(UserModel userModel) {

            var user = Mapper.Map(userModel).ToANew<User>();
            user.IsEnabled = true;
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            user.TokenCreatedDate = DateTime.Now;
            user.TokenExpiresDate = DateTime.Now.AddDays(int.Parse(_configuration["Jwt:ExpireHours"]));
            _userRepository.Save(user);
            userModel = Mapper.Map(user).ToANew<UserModel>();
            return userModel;
        }

        public UserModel UpdateToken(int id, string token) {

            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id.Equals(id));
            if (user == null) {
                return null;
            }
            user.Token = token;
            user.TokenCreatedDate = DateTime.Now;
            user.TokenExpiresDate = DateTime.Now.AddDays(int.Parse(_configuration["Jwt:ExpireHours"]));
            _userRepository.Update(user);
            _userRepository.Save();
            var userModel = Mapper.Map(user).ToANew<UserModel>();
            return userModel;

        }

        public UserModel? Login(string email, string password) {
            //var user = _userRepository.GetAll().FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password) && x.TokenExpiresDate >= DateTime.Now);
            //return user == null ? null : Mapper.Map(user).ToANew<UserModel>();
            return null;
        }
    }
}

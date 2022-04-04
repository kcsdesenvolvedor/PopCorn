using Microsoft.IdentityModel.Tokens;
using PopCorn.Web.Domain;
using PopCorn.Web.Helpers;
using PopCorn.Web.Infra.Repositories.Interfaces;
using PopCorn.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PopCorn.Web.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User Authenticate(string username, string password)
        {
            var user = GetUserByName(username, password);

            if (user == null )
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;
            user.DateCreate = DateTime.UtcNow;


            return user;
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public User GetUserByName(string name, string password)
        {
            return _userRepository.GetByUsername(name, password);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public void Save(User user)
        {
            if (user != null)
            {
                _userRepository.Save(user);
            }
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}

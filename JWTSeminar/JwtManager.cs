using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTSeminar
{
    public class JwtManager : IJwtAuthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>()
        {
            {"user1","password1"},
            {"user2","password2"},
            {"user3","password3"},
            {"user4","password4"}
        };
        private readonly string _key;

        public JwtManager(string key)
        {
            _key = key;
            
        }

        public string Authenticate(UserCrendetial user)
        {

            var result = users.First(x=>x.Key==user.Username && x.Value == user.Password);
            if (result.Value == null)
            {
                return null; 
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

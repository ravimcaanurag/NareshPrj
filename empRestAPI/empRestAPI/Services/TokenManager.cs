using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly string key;
        private readonly string expDate;
        private readonly ILog logService;

        public TokenManager(IConfiguration config, ILog _logService)
        {
            key = config.GetSection("JwtConfig").GetSection("secret").Value;
            expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
            logService = _logService;
        }
        public async Task<string> GenerateToken(string user)
        {
            string jwtToken = "";
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, user)
              }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                jwtToken = await Task.FromResult(tokenHandler.WriteToken(token));
                logService.Log(LogService.GenerateToken, "Token Generated", "Info");
            }
            catch (Exception ex)
            {
                logService.Log(LogService.GenerateToken, ex.Message, "Error");
            }
            return jwtToken;
        }
    }
}

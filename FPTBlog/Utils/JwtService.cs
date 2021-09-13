
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FPTBlog.Utils.Interface;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace FPTBlog.Utils
{
    public class JwtService : IJwtService
    {
        private readonly string Secret;
        private readonly IConfig Config;
        public JwtService(IConfig config)
        {
            this.Config = config;
            this.Secret = this.Config.GetEnvByKey("JWT_SECRET");
        }

        public string GenerateToken(string data)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("data", data) }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken verifiedToken);
                // I have no idea what am I doing here :)
                var jwtToken = (JwtSecurityToken)verifiedToken;
                return jwtToken.Claims.First(x => x.Type == "data").Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public JwtSecurityToken Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var data = handler.ReadJwtToken(token);
            return data;
        }

        public object GetDataFromJwtToken(JwtSecurityToken token, string field)
        {
            object value;
            if (token.Payload.TryGetValue(field, out value))
            {
                return value;
            }
            return null;
        }
    }
}

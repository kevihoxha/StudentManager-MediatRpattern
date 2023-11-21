using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestingMediatR.Models;
using Microsoft.Extensions.Options;
using TestingMediatR.Data;

namespace TestingMediatR.JWT
{
    internal sealed class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Generate(StudentDetails student)
        {
            var claims = new Claim[] {
            new (JwtRegisteredClaimNames.Sub , student .Id.ToString()),
            new (JwtRegisteredClaimNames.Email , student .StudentEmail),
             new Claim(ClaimTypes.Role, "Admin"),
             new Claim(CustomClaimTypes.Permission, "CanViewStudentDetails"),
            };
            var securityKey = new SymmetricSecurityKey(
                                  Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value));
            var signingCredentials = new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt:Issuer").Value,
                _configuration.GetSection("Jwt:Audience").Value,
                claims,
                null,
                DateTime.UtcNow.AddHours(9),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}

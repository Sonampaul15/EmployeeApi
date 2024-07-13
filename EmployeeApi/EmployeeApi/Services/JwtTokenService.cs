using EmployeeApi.Models;
using EmployeeApi.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
  
namespace EmployeeApi.Services
{
    public class JwtTokenService : IJwtTokenGenerator
    {
        private readonly JWTOptions jwtoption;
        public JwtTokenService(IOptions<JWTOptions> _jwtoption)
        {
            jwtoption = _jwtoption.Value;
        }
        public string GenerateToken(ApplicationUser application, IEnumerable<string> roles)
        {
            var tokenHndeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtoption.Secret);
            var claims = new List<Claim>()
            {
                new Claim (JwtRegisteredClaimNames.Sub, application.Id),
                new Claim (JwtRegisteredClaimNames.Name,application.FirstName),
                new Claim (JwtRegisteredClaimNames.Email,application.Email),
            };
            claims.AddRange(roles.Select(roles=> new Claim(ClaimTypes.Role,roles)));

            var tokenDescrpitor = new SecurityTokenDescriptor
            {
                Audience = jwtoption.Audience,
                Issuer = jwtoption.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHndeler.CreateToken(tokenDescrpitor);
            return tokenHndeler.WriteToken(token);
        }
    }
}

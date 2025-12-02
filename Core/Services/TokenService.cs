using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Core.Interfaces;

namespace Core.Services
{
    public class TokenService : ITokenService
    {
        private const string SecretKey = "4J9PzQp7yXwT1fR6vBcD3aL8mH2gN0kS5uZtYxOeW4iGjV9hFqK7eA1cZ";

        public string GenerateJwt(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email)
            };

            // Якщо хочеш додати ім’я/фамілію:
            if (!string.IsNullOrEmpty(user.FirstName))
                claims.Add(new Claim("firstName", user.FirstName));

            if (!string.IsNullOrEmpty(user.LastName))
                claims.Add(new Claim("lastName", user.LastName));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Core.Interfaces.Auth;
using Core.Models;
using Core.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class JwtService : IJwtService
    {
        private const string SecretKey = "YourSuperSecretKeyThatMustBeAtLeast32BytesLongForHmacSha256";
        private const int TokenExpiryDays = 7;

        public Task<TokenResultDto> GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            string fullName = $"{user.FirstName} {user.LastName}".Trim();
            if (string.IsNullOrWhiteSpace(fullName))
                fullName = user.Email;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, fullName)
                }),
                Expires = DateTime.UtcNow.AddDays(TokenExpiryDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(new TokenResultDto
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = (int)TimeSpan.FromDays(TokenExpiryDays).TotalSeconds
            });
        }
    }
}

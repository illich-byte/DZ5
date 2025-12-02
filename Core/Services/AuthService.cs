using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _googleClientId;

        public AuthService(IConfiguration configuration)
        {
            _googleClientId = configuration["Authentication:Google:ClientId"];

            if (string.IsNullOrEmpty(_googleClientId))
                throw new InvalidOperationException("Google Client ID не знайдено!");
        }

        public async Task<AuthResultDto> GoogleSignInAsync(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { _googleClientId }
                    });

                var fakeToken = $"FAKE_JWT_{payload.Email}_{DateTime.UtcNow.Ticks}";

                return new AuthResultDto
                {
                    Success = true,
                    Token = fakeToken,
                    ExpiresIn = 3600,
                    UserInfo = new UserInfoDto
                    {
                        Id = payload.Subject,
                        Email = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        PictureUrl = payload.Picture
                    }
                };
            }
            catch
            {
                return new AuthResultDto
                {
                    Success = false,
                    Errors = { "Невалідний Google Token" }
                };
            }
        }
    }
}

using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Models; // Використовуємо AppUser з Core.Models
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
namespace Core.Services
{
    /// <summary>
    /// Сервіс для обробки входу/реєстрації через Google OAuth.
    /// Клас реалізує IAuthService.
    /// </summary>
    public class GoogleAuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly string _googleClientId;

        public GoogleAuthService(
            IUserService userService,
            IJwtService jwtService,
            IConfiguration configuration)
        {
            _userService = userService;
            _jwtService = jwtService;

            // Зчитування Client ID з конфігурації для валідації токена
            _googleClientId = configuration["GoogleAuth:ClientId"]
                ?? throw new InvalidOperationException("GoogleAuth:ClientId не знайдено в конфігурації.");
        }

        /// <summary>
        /// Обробляє вхід або реєстрацію користувача за допомогою Google ID Token.
        /// </summary>
        /// <param name="idToken">ID Token, отриманий від клієнта після успішної автентифікації Google.</param>
        /// <returns>AuthResultDto з JWT та інформацією про користувача, або null у разі помилки.</returns>
        public async Task<AuthResultDto?> GoogleSignInAsync(string idToken)
        {
            GoogleJsonWebSignature.Payload googlePayload;
            try
            {
                // Налаштування валідації: Audience повинен відповідати Client ID вашого додатку
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleClientId }
                };

                // 1. Валідація Google ID Token
                googlePayload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);
            }
            catch (InvalidJwtException)
            {
                // Недійсний токен
                return null;
            }

            // 2. Пошук користувача в базі за Email
            var user = await _userService.GetUserByEmailAsync(googlePayload.Email!);

            if (user == null)
            {
                // 3. Створення нового користувача, якщо він не знайдений
                var newUserDto = new UserInfoDto
                {
                    Email = googlePayload.Email!,
                    FirstName = googlePayload.GivenName,
                    LastName = googlePayload.FamilyName,
                    PictureUrl = googlePayload.Picture
                };

                user = await _userService.CreateUserAsync(newUserDto);

                if (user == null)
                {
                    // Помилка створення користувача
                    return null;
                }
            }
            else
            {
                // 4. Оновлення часу останнього входу
                await _userService.UpdateLastLoginAsync(user);
            }

            // 5. Генерація внутрішнього JWT для авторизації
            // Викликаємо IJwtService.GenerateToken, який повертає TokenResultDto
            var tokenResult = await _jwtService.GenerateToken(user);

            // 6. Побудова та повернення результату
            return new AuthResultDto
            {
                Token = tokenResult.Token,
                ExpiresIn = tokenResult.ExpiresIn,
                UserInfo = new UserInfoDto
                {
                    Id = user.Id, // Використовуємо Id з AppUser
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PictureUrl = user.PictureUrl
                }
            };
        }
    }
}
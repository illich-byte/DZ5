using Core.DTOs;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    /// <summary>
    /// Інтерфейс для сервісу генерації JWT-токенів (JSON Web Token).
    /// Він використовується іншими сервісами, наприклад, GoogleAuthService,
    /// для створення токенів після успішної аутентифікації.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Асинхронно генерує JWT для вказаного користувача.
        /// </summary>
        /// <param name="user">Об'єкт користувача (AppUser), для якого генерується токен.</param>
        /// <returns>Task, що містить TokenResultDto з токеном та терміном дії (ExpiresIn).</returns>
        Task<TokenResultDto> GenerateToken(AppUser user);
    }
}
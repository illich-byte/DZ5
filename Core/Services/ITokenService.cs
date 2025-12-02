using Core.Models;

namespace Core.Interfaces
{
    /// <summary>
    /// Інтерфейс для сервісу генерації JWT.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Генерує JWT-токен для вказаного користувача.
        /// </summary>
        /// <param name="user">Об'єкт користувача AppUser.</param>
        /// <returns>Згенерований JWT як рядок.</returns>
        string GenerateJwt(AppUser user);
    }
}
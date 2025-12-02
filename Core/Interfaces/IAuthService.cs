using Core.DTOs;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    /// <summary>
    /// Базовий інтерфейс для служб автентифікації.
    /// </summary>
    public interface IAuthService
    {
        Task<AuthResultDto?> GoogleSignInAsync(string idToken);
    }
}
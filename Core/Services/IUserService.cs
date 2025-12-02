using Core.DTOs;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces // Змінено: Інтерфейси зазвичай знаходяться у просторі імен Interfaces
{
    /// <summary>
    /// Інтерфейс для керування користувачами та їхніми даними.
    /// Методи є асинхронними для роботи з Identity/базою даних.
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// Асинхронно отримує користувача за його електронною поштою.
        /// </summary>
        Task<AppUser?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Асинхронно створює нового користувача в системі, використовуючи наданий DTO.
        /// </summary>
        Task<AppUser?> CreateUserAsync(UserInfoDto userInfoDto);

        /// <summary>
        /// Асинхронно оновлює позначку часу останнього входу користувача.
        /// </summary>
        Task UpdateLastLoginAsync(AppUser user);
    }
}
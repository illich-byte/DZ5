using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Domain;
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbTransferContext _context;

        public UserService(UserManager<AppUser> userManager, AppDbTransferContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // --- Реалізація IUserService ---

        /// <summary>
        /// Асинхронно отримує користувача за його електронною поштою.
        /// </summary>
        public async Task<AppUser?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            // Використовуємо FindByEmailAsync з UserManager для Identity-сумісних моделей
            return await _userManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Асинхронно створює нового користувача в системі.
        /// </summary>
        public async Task<AppUser?> CreateUserAsync(UserInfoDto userInfoDto)
        {
            var newUser = new AppUser
            {
                Email = userInfoDto.Email,
                UserName = userInfoDto.Email, // Використовуємо Email як UserName для Identity
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                PictureUrl = userInfoDto.PictureUrl,
                EmailConfirmed = true,
                RegistrationDate = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow // Додайте цю властивість до класу AppUser!
            };

            // Створюємо користувача через Identity
            var result = await _userManager.CreateAsync(newUser);

            if (result.Succeeded)
            {
                // Приклад: Додаємо стандартну роль "User"
                await _userManager.AddToRoleAsync(newUser, "User");
                return newUser;
            }

            // Якщо не вдалося створити (наприклад, дублікат email)
            return null;
        }

        /// <summary>
        /// Асинхронно оновлює позначку часу останнього входу користувача.
        /// </summary>
        public async Task UpdateLastLoginAsync(AppUser user)
        {
            // Це оновлення часто має бути на рівні бази даних
            if (user == null) return;

            // Оновлюємо властивість LastLogin
            user.LastLogin = DateTime.UtcNow;

            // Оскільки AppUser керується UserManager, використовуємо його для оновлення
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                // Обробка помилок оновлення, якщо потрібно
                Console.WriteLine($"Error updating LastLogin for user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
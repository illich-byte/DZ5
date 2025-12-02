using System.Collections.Generic;
using System.Linq;

namespace Core.DTOs
{
    public class AuthResultDto
    {
        public bool Success { get; set; } = true;

        // JWT токен, рядок
        public string Token { get; set; }

        // Час дії токена у секундах (int)
        public int ExpiresIn { get; set; }

        // Інформація про користувача
        public UserInfoDto UserInfo { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Any();
    }
}

namespace Core.DTOs
{
    /// <summary>
    /// Модель для прийому зовнішнього токена (наприклад, Google ID Token) від клієнта.
    /// </summary>
    public class ExternalLoginDto
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
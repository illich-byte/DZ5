namespace Core.DTOs
{
    public class TokenResultDto
    {
        /// <summary>JWT токен</summary>
        public string Token { get; set; }

        /// <summary>Час дії у секундах</summary>
        public int ExpiresIn { get; set; }
    }
}

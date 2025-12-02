using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class GoogleLoginDto
    {
        [Required]
        public string IdToken { get; set; }
    }
}
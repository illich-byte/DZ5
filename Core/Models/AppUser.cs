using System;

namespace Core.Models
{
    public class AppUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }

        public bool EmailConfirmed { get; set; } = false;
    }
}

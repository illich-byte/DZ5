namespace Core.DTOs
{
    public class UserInfoDto
    {
        public string Id { get; set; }       // Google ID або внутрішній ID
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}

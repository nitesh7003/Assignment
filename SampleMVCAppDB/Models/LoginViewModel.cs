namespace SampleMVCAppDB.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";  // Default role assigned as "User"

    }
}

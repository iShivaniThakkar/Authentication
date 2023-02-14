using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }

    public class LoginView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

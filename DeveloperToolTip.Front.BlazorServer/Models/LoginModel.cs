using System.ComponentModel.DataAnnotations;

namespace DeveloperToolTip.Front.BlazorServer.Models
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

        public string? IpAdress { get; set; }
    }
}

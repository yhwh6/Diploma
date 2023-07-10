using System.ComponentModel.DataAnnotations;

namespace Diploma.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public enum UserRole
    {
        Guest,
        Administrator
    }
}

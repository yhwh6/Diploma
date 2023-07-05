namespace Diploma.Models
{
    public class User
    {
        public int ID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string Role { get; set; }
    }

    public enum UserRole
    {
        Guest,
        Administrator
    }
}

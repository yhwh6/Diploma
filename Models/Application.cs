namespace Diploma.Models
{
    public class Application
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Message { get; set; }
        public ApplicationStatus Status { get; set; }
    }

    public enum ApplicationStatus
    {
        Received,
        InProgress,
        Completed,
        Rejected,
        Canceled
    }
}

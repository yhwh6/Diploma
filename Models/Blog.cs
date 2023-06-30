namespace Diploma.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}

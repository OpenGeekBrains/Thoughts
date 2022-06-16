namespace Thoughts.Services.Models
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
    }
}

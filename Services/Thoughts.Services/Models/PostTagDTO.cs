namespace Thoughts.Services.Models
{
    public struct PostTagDTO
    {
        public int PostId { get; set; }
        public string Tag { get; set; }
    }
    public struct CreatePostDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
    }
}

namespace Thoughts.Services.Models
{
    public class UserPostsPageDTO
    {
        public string UserId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

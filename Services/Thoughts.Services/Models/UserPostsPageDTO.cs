namespace Thoughts.Services.Models
{
    public struct UserPostsPageDTO
    {
        public string UserId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

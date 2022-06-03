namespace Thoughts.Services.Models
{
    public struct UserPostsSkipTakeDTO
    {
        public string UserId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

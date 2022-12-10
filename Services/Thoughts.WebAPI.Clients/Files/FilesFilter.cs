namespace Thoughts.WebAPI.Clients.Files
{
    public class FilesFilter
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public OrderByType OrderByType { get; set; }

        public static readonly FilesFilter Default = new FilesFilter
        {
            Page = 1,
            PageSize = 5,
            OrderByType = OrderByType.ByCreatedTime,
        };
    }

    public enum OrderByType
    {
        Default,
        ByName,
        ByNameDesc,
        ByCreatedTime,
        ByCreatedTimeDesc,
        ByHash,
        ByHashDesc,
        BySize,
        BySizeDesc,
        ByCountLinks,
        ByCountLinksDesc,
    }
}

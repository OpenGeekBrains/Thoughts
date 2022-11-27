namespace Thoughts.WebAPI.Clients.Files
{
    public class FilesFilter
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public OrderByType OrderByType { get; set; }
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

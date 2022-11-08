namespace Thoughts.WebAPI.Clients.Files
{
    public interface IFilesService
    {
        Task<bool> UploadLimitSizeFileAsync(Stream stream, string fileName, string contentType, CancellationToken token = default);

        bool UploadLimitSizeFile(Stream stream, string fileName, string contentType);

        Task<bool> UploadAnyFileAsync(Stream stream, string fileName, string contentType, CancellationToken token = default);

        bool UploadAnyFile(Stream stream, string fileName, string contentType);

        Task<IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)>> GetFilesAsync(int page = default, int pageSize = default, CancellationToken token = default);

        IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> GetFiles(int page = default, int pageSize = default);
    }
}

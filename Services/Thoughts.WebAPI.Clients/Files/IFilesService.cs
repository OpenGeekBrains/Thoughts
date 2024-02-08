namespace Thoughts.WebAPI.Clients.Files
{
    public interface IFilesService
    {
        Task<bool> UploadLimitSizeFileAsync(Stream stream, string fileName, string contentType, CancellationToken token = default);

        Task<bool> UploadAnyFileAsync(Stream stream, string fileName, string contentType, CancellationToken token = default);

        Task<(IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> Files, int TotalCount)> GetFilesAsync(FilesFilter filter = default, CancellationToken token = default);

        Task<bool> DeleteFileAsync(string hash, CancellationToken token = default);

        Task<bool> DeactivateFileAsync(string hash, CancellationToken token = default);

        Task<bool> ActivateFileAsync(string hash, CancellationToken token = default);

        bool DeleteFile(string hash);

        bool DeactivateFile(string hash);

        bool ActivateFile(string hash);

        bool UploadLimitSizeFile(Stream stream, string fileName, string contentType); 

        bool UploadAnyFile(Stream stream, string fileName, string contentType); 

        (IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> Files, int TotalCount) GetFiles(FilesFilter filter = default);
    }
}

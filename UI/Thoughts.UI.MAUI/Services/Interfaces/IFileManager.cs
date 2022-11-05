namespace Thoughts.UI.MAUI.Services.Interfaces
{
    public interface IFileManager
    {
        Task<bool> UploadLimitSizeFileAsync(FileResult file, CancellationToken token = default);

        Task<bool> UploadAnyFileAsync(FileResult file, CancellationToken token = default);

        Task<object> GetFilesAsync(int page = default, CancellationToken token = default);

        object GetFiles(int page = default);

        bool UploadLimitSizeFile(FileResult file);

        bool UploadAnyFile(FileResult file);
    }
}

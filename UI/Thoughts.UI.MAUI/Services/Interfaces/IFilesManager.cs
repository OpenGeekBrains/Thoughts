using Thoughts.UI.MAUI.ViewModels;

namespace Thoughts.UI.MAUI.Services.Interfaces
{
    public interface IFilesManager
    {
        Task<bool> UploadLimitSizeFileAsync(FileResult file, CancellationToken token = default);

        Task<bool> UploadAnyFileAsync(FileResult file, CancellationToken token = default);

        Task<(IEnumerable<FileViewModel> Files, int TotalPages)> GetFilesAsync(int page = 1, CancellationToken token = default);

        Task<bool> DeleteFileAsync(string hash, CancellationToken token = default);

        Task<bool> ActivateFileAsync(string hash, CancellationToken token = default);

        Task<bool> DeactivateFileAsync(string hash, CancellationToken token = default);
    }
}

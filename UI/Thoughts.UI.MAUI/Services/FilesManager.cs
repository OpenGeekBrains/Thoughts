using Microsoft.Extensions.Logging;

using Thoughts.UI.MAUI.Services.Interfaces;
using Thoughts.UI.MAUI.ViewModels;
using Thoughts.WebAPI.Clients.Files;

namespace Thoughts.UI.MAUI.Services
{
    public class FilesManager : IFilesManager
    {
        #region Fields

        private readonly IFilesService _filesService;
        private readonly ILogger<FilesManager> _logger;
        private readonly AppSettings.PageSettings _pageSettings;

        #endregion

        #region Constructors

        public FilesManager(IFilesService filesService, 
            AppSettings appSettings,
            ILogger<FilesManager> logger)
        {
            _filesService = filesService;
            _pageSettings = appSettings.Page;
            _logger = logger;
        }

        #endregion

        #region IFileManager implementation

        public async Task<bool> UploadLimitSizeFileAsync(FileResult file, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            using var stream = await file.OpenReadAsync().ConfigureAwait(false);

            if(stream is null) return false;

            var result = await _filesService.UploadLimitSizeFileAsync(stream, file.FileName, file.ContentType, token).ConfigureAwait(false);

            return result;
        }

        public async Task<bool> UploadAnyFileAsync(FileResult file, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            using var stream = await file.OpenReadAsync().ConfigureAwait(false);

            if (stream is null) return false;

            var result = await _filesService.UploadAnyFileAsync(stream, file.FileName, file.ContentType, token).ConfigureAwait(false);

            return result;
        }

        public async Task<(IEnumerable<FileViewModel> Files, int TotalPages)> GetFilesAsync(int page = 1, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            if (page < 1)
            {
                _logger.LogWarning("{Method}: Page value can't be less than \"1\". Changing page value on \"1\"", nameof(GetFilesAsync));
                page = 1;
            }

            var fileFilter = new FilesFilter 
            { 
                Page = page, 
                PageSize = _pageSettings.PageSize,
                OrderByType = OrderByType.ByCreatedTimeDesc
            };

            var (files, totalCount) = await _filesService.GetFilesAsync(fileFilter, token).ConfigureAwait(false);

            var result = files.Select(f => new FileViewModel
            {
                Hash = f.Hash,
                Name = f.Name,
                LinksCount = f.Counter,
                Size = f.Size,
                Created = f.Created,
                Active = f.Active
            });

            var totalPages = (int) Math.Ceiling((double) totalCount / _pageSettings.PageSize);

            return (result, totalPages);
        }

        public async Task<bool> DeleteFileAsync(string hash, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(hash))
            {
                _logger.LogError("{Method}: Hash file is null or empty", nameof(DeleteFileAsync));
                throw new ArgumentNullException(nameof(hash));
            }

            var result = await _filesService.DeleteFileAsync(hash, token).ConfigureAwait(false);

            return result;
        }

        public async Task<bool> ActivateFileAsync(string hash, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(hash))
            {
                _logger.LogError("{Method}: Hash file is null or empty", nameof(ActivateFileAsync));
                throw new ArgumentNullException(nameof(hash));
            }

            var result = await _filesService.ActivateFileAsync(hash, token).ConfigureAwait(false);

            return result;
        }

        public async Task<bool> DeactivateFileAsync(string hash, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(hash))
            {
                _logger.LogError("{Method}: Hash file is null or empty", nameof(DeactivateFileAsync));
                throw new ArgumentNullException(nameof(hash));
            }

            var result = await _filesService.DeactivateFileAsync(hash, token).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}

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

        public async Task<IEnumerable<FileViewModel>> GetFilesAsync(int page = default, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            var files = await _filesService.GetFilesAsync(page, _pageSettings.PageSize, token).ConfigureAwait(false);

            var result = files.Select(f => new FileViewModel
            {
                Hash = f.Hash,
                Name = f.Name,
                LinksCount = f.Counter,
                Size = f.Size,
                Created = f.Created,
                Active = f.Active
            });

            return result;
        }

        #region Sync versions

        public bool UploadLimitSizeFile(FileResult file) => UploadLimitSizeFileAsync(file).Result;

        public bool UploadAnyFile(FileResult file) => UploadAnyFileAsync(file).Result;

        public IEnumerable<FileViewModel> GetFiles(int page = default) => GetFilesAsync(page).Result;

        #endregion

        #endregion
    }
}

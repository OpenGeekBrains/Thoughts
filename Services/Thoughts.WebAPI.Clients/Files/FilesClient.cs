using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Logging;

using Thoughts.Interfaces.Base;
using Thoughts.WebAPI.Clients.Tools;

namespace Thoughts.WebAPI.Clients.Files
{
    public class FilesClient : IFilesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FilesClient> _logger;

        public FilesClient(HttpClient httpClient, ILogger<FilesClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        #region IFileService implementation

        public async Task<bool> UploadLimitSizeFileAsync(Stream stream, string fileName, string contentType, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogInformation("{Method}: upload limit size file \"{name}\"", nameof(UploadLimitSizeFileAsync), fileName);

            using var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            using var form = new MultipartFormDataContent();
            form.Add(streamContent, fileName[..fileName.IndexOf('.')], fileName);
            form.DeleteQuotesFromHeader("boundary");

            var response = await _httpClient.PostAsync($"{WebApiControllersPath.FileUrl}/upload", form, token);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UploadAnyFileAsync(Stream stream, string fileName, string contentType, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogInformation("{Method}: upload no limit size file \"{name}\"", nameof(UploadAnyFileAsync), fileName);

            using var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            using var form = new MultipartFormDataContent();
            form.Add(streamContent, fileName[..fileName.IndexOf('.')], fileName);
            form.DeleteQuotesFromHeader("boundary");

            var response = await _httpClient.PostAsync($"{WebApiControllersPath.FileUrl}/uploadlarge", form, token);

            return response.IsSuccessStatusCode;
        }

        public async Task<(IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> Files, int TotalCount)> GetFilesAsync(
            FilesFilter filter = default,
            CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogDebug("{Method}: get files request for page = {pageNum} with pageSize = {pageSize}", nameof(GetFilesAsync), filter?.Page, filter?.PageSize);

            var jsonElements = await _httpClient
                .GetFromJsonAsync<IEnumerable<JsonElement>>($"{WebApiControllersPath.FileUrl}/getallfileinfo", token)
                .ConfigureAwait(false);

            var totalCount = jsonElements.Count();

            //для сортировки преобразуем сразу все файлы в нужный формат, т.к. от хоста передается анонимный класс
            var pageFiles = FromJsonToFiles(jsonElements);
            
            if (filter is { Page: > 0 and var pageNumber, PageSize: > 0 and var pageSize})
            {
                pageFiles = OrderBy(jsonElements, filter.OrderByType)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            return (pageFiles, totalCount);
        }

        public async Task<bool> DeleteFileAsync(string hash, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogWarning("{Method}: delete file with hash = \"{hash}\"", nameof(DeleteFileAsync), hash);

            var result = await _httpClient
                .DeleteAsync($"{WebApiControllersPath.FileUrl}/delete?file={hash}", token)
                .ConfigureAwait(false);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeactivateFileAsync(string hash, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogInformation("{Method}: deactivate file with hash = \"{hash}\"", nameof(DeactivateFileAsync), hash);

            var jsonElement = await _httpClient
                .GetFromJsonAsync<JsonElement>($"{WebApiControllersPath.FileUrl}/softdelete?file={hash}", token)
                .ConfigureAwait(false);

            var result = FromJsonToStatusResult(jsonElement);

            return result;
        }

        public async Task<bool> ActivateFileAsync(string hash, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogInformation("{Method}: activate file with hash = \"{hash}\"", nameof(ActivateFileAsync), hash);

            var jsonElement = await _httpClient
                .GetFromJsonAsync<JsonElement>($"{WebApiControllersPath.FileUrl}/activatefile?file={hash}", token)
                .ConfigureAwait(false);

            var result = FromJsonToStatusResult(jsonElement);

            return result;
        }

        public bool DeleteFile(string hash) => DeleteFileAsync(hash).Result;

        public bool DeactivateFile(string hash) => DeactivateFileAsync(hash).Result;

        public bool ActivateFile(string hash) => ActivateFileAsync(hash).Result;

        public bool UploadLimitSizeFile(Stream stream, string fileName, string contentType) =>
           UploadLimitSizeFileAsync(stream, fileName, contentType).Result;

        public bool UploadAnyFile(Stream stream, string fileName, string contentType) =>
            UploadAnyFileAsync(stream, fileName, contentType).Result;

        public (IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> Files, int TotalCount) GetFiles(FilesFilter filter = default) =>
            GetFilesAsync(filter).Result;

        #endregion

        #region Methods

        private IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> FromJsonToFiles(IEnumerable<JsonElement> jsonElements, CancellationToken token = default)
        {
            if (jsonElements is null)
                throw new ArgumentNullException(nameof(jsonElements));

            var result = jsonElements.Select(je => GetFileInfo(je));

            return result;
        }

        private bool FromJsonToStatusResult(JsonElement jsonElement)
        {
            var result = jsonElement
                .GetProperty("result")
                .GetBoolean();

            return result;
        }

        private (string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active) GetFileInfo(JsonElement element)
        {
            var hash = element
                .GetProperty("hash")
                .GetString() ?? string.Empty;

            var name = element
                .GetProperty("name")
                .GetString() ?? string.Empty;

            var counter = element
                .GetProperty("counter")
                .TryGetInt32(out var counterRes) ? counterRes : default;

            var size = element
                .GetProperty("size")
                .TryGetInt64(out var sizeRes) ? sizeRes : default;

            var created = element
                .GetProperty("created")
                .TryGetDateTimeOffset(out var createdRes) ? createdRes : default;

            var active = element
                .GetProperty("active")
                .GetBoolean();

            return (hash, name, counter, size, created, active);
        }

        private IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> OrderBy(
            IEnumerable<JsonElement> jsonElements,
            OrderByType orderByType) => 
            orderByType switch
            {
                OrderByType.Default => FromJsonToFiles(jsonElements),
                OrderByType.BySize => FromJsonToFiles(jsonElements).OrderBy(item => item.Size),
                OrderByType.BySizeDesc => FromJsonToFiles(jsonElements).OrderByDescending(item => item.Size),
                OrderByType.ByName => FromJsonToFiles(jsonElements).OrderBy(item => item.Name),
                OrderByType.ByNameDesc => FromJsonToFiles(jsonElements).OrderByDescending(item => item.Name),
                OrderByType.ByCreatedTime => FromJsonToFiles(jsonElements).OrderBy(item => item.Created),
                OrderByType.ByCreatedTimeDesc => FromJsonToFiles(jsonElements).OrderByDescending(item => item.Created),
                OrderByType.ByCountLinks => FromJsonToFiles(jsonElements).OrderBy(item => item.Counter),
                OrderByType.ByCountLinksDesc => FromJsonToFiles(jsonElements).OrderByDescending(item => item.Counter),
                OrderByType.ByHash => FromJsonToFiles(jsonElements).OrderBy(item => item.Hash),
                OrderByType.ByHashDesc => FromJsonToFiles(jsonElements).OrderByDescending(item => item.Hash),
                _ => FromJsonToFiles(jsonElements),
            };

        private IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> OrderBy(
            IEnumerable<(string Hash, string Name, int Counter, long Size, DateTimeOffset Created, bool Active)> elements,
            OrderByType orderByType) =>
            orderByType switch
            {
                OrderByType.Default => elements,
                OrderByType.BySize => elements.OrderBy(item => item.Size),
                OrderByType.BySizeDesc => elements.OrderByDescending(item => item.Size),
                OrderByType.ByName => elements.OrderBy(item => item.Name),
                OrderByType.ByNameDesc => elements.OrderByDescending(item => item.Name),
                OrderByType.ByCreatedTime => elements.OrderBy(item => item.Created),
                OrderByType.ByCreatedTimeDesc => elements.OrderByDescending(item => item.Created),
                OrderByType.ByCountLinks => elements.OrderBy(item => item.Counter),
                OrderByType.ByCountLinksDesc => elements.OrderByDescending(item => item.Counter),
                OrderByType.ByHash => elements.OrderBy(item => item.Hash),
                OrderByType.ByHashDesc => elements.OrderByDescending(item => item.Hash),
                _ => elements,
            };

        #endregion
    }
}

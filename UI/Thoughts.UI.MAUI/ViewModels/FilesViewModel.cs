using System.Collections.ObjectModel;
using System.Windows.Input;

using Microsoft.Extensions.Logging;

using Thoughts.UI.MAUI.Services.Interfaces;
using Thoughts.UI.MAUI.ViewModels.Base;

namespace Thoughts.UI.MAUI.ViewModels
{
    public class FilesViewModel : ViewModel
    {
        #region Fields

        private readonly IFilesManager _fileManager;
        private readonly IConnectivity _connectivity;
        private readonly ILogger<FilesViewModel> _logger;

        private readonly PickOptions _pickOptions;

        #endregion

        #region Bindable properties

        public ObservableCollection<FileViewModel> Files { get; } = new();

        private int _currentPageNum;

        public int CurrentPageNum
        {
            get => _currentPageNum;

            set => Set(ref _currentPageNum, value);
        }

        private int _totalPagesCount;

        public int TotalPagesCount
        {
            get => _totalPagesCount;

            set => Set(ref _totalPagesCount, value);
        }

        private bool _isRefresh;

        public bool IsRefreshing
        {
            get => _isRefresh;

            set => Set(ref _isRefresh, value);
        }

        private FileViewModel _selectedFile;

        public FileViewModel SelectedFile
        {
            get => _selectedFile;

            set => Set(ref _selectedFile, value);
        }

        #endregion

        #region Constructors

        public FilesViewModel(IFilesManager fileManager, 
            IConnectivity connectivity,
            ILogger<FilesViewModel> logger = default)
        {
            _fileManager = fileManager;
            _connectivity = connectivity;
            _logger = logger;

            Title = "Файлы";

            _pickOptions = new PickOptions
            {
                PickerTitle = "Выберите файл",
            };
        }

        #endregion

        #region Commands

        #region Upload image

        ICommand _uploadImageCommand;

        public ICommand UploadImageCommand => _uploadImageCommand ??= new Command(OnUploadImageAsync); 

        private async void OnUploadImageAsync()
        {
            if (IsBusy) return;

            try
            {
                if (!await CheckInternetConnectionAsync() || !await TryRequestStorageReadPermissions()) return;

                IsBusy = true;

                _pickOptions.FileTypes = FilePickerFileType.Images;

                var file = await FilePicker.Default.PickAsync(_pickOptions);

                if (file is null) return;

                var result = await _fileManager.UploadLimitSizeFileAsync(file);

                _logger?.LogInformation("{Method}: upload is {success}", nameof(OnUploadImageAsync), result);

                var resultMsg = result ? "успешно" : "с ошибкой";

                await Shell.Current.DisplayAlert("Загрузка файла",
                    $"Загрузка {file.FileName} завершилась {resultMsg}!", "OK");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "{Method}: {message}", nameof(OnUploadImageAsync), ex.Message);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to upload file: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region Upload any file

        ICommand _uploadAnyCommand;

        public ICommand UploadAnyCommand => _uploadAnyCommand ??= new Command(OnUploadAnyAsync);

        private async void OnUploadAnyAsync()
        {
            if (IsBusy) return;

            try
            {
                if (!await CheckInternetConnectionAsync() || !await TryRequestStorageReadPermissions()) return;

                IsBusy = true;

                _pickOptions.FileTypes = default;

                var file = await FilePicker.Default.PickAsync(_pickOptions);

                if (file is null) return;

                var result = await _fileManager.UploadAnyFileAsync(file);

                _logger?.LogInformation("{Method}: upload is {success}", nameof(OnUploadAnyAsync), result);

                var resultMsg = result ? "успешно" : "с ошибкой";

                await Shell.Current.DisplayAlert("Загрузка файла",
                    $"Загрузка {file.FileName} завершилась {resultMsg}!", "OK");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "{Method}: {message}", nameof(OnUploadAnyAsync), ex.Message);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to upload file: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region Get page files

        ICommand _getFilesPageCommand;

        public ICommand GetFilesPageCommand => _getFilesPageCommand
            ??= new Command(GetFilesPageAsync);

        private async void GetFilesPageAsync()
        {
            if (IsBusy) return;

            try
            {
                if (!await CheckInternetConnectionAsync()) return;

                IsBusy = true;

                var files = await _fileManager.GetFilesAsync();

                if (Files.Count > 1)
                    Files.Clear();

                foreach (var file in files)
                {
                    Files.Add(file);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "{Method}: {message}", nameof(GetFilesPageAsync), ex.Message);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get files: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        #endregion

        #region Change file active status

        ICommand _changeFileActiveCommand;

        public ICommand ChangeFileActiveCommand => _changeFileActiveCommand ??= new Command(OnChangeActive);

        private async void OnChangeActive(object obj)
        {
            try
            {
                var result = default(bool);

                var file = obj as FileViewModel;

                if (file is null)
                {
                    _logger?.LogWarning("{Method}: selected file is null", nameof(OnChangeActive));
                    await Shell.Current.DisplayAlert("Ошибка!",
                        $"Сначала выберите файл", "OK");

                    return;
                }

                if (file.Active)
                {
                    result = await _fileManager.DeactivateFileAsync(file.Hash);

                    file.Active = !result;

                    return;
                }

                result = await _fileManager.ActivateFileAsync(file.Hash);

                file.Active = result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "{Method}: {message}", nameof(OnChangeActive), ex.Message);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get files: {ex.Message}", "OK");
            }
            finally
            {
                SelectedFile = null;
            }
        }

        #endregion

        #endregion

        #region Methods

        private async Task<bool> CheckInternetConnectionAsync(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            if (_connectivity.NetworkAccess == NetworkAccess.Internet) return true;

            _logger?.LogError("{Method}: {message}", nameof(CheckInternetConnectionAsync), "Check your internet connection");
            await Shell.Current.DisplayAlert("Internet connection failed!",
                $"Unable to upload file: Check your internet connection", "OK");

            return false;
        }

        private async Task<bool> TryRequestStorageReadPermissions(CancellationToken token = default) 
        {
            //Check current permission status
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (CheckPermissionsOnGranted(permissionStatus))
            {
                _logger?.LogInformation("{Method}: Read storage permissions obtained", nameof(TryRequestStorageReadPermissions));
                return true;
            }

            //Trying to get permission from the user
            permissionStatus = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (CheckPermissionsOnGranted(permissionStatus))
            {
                _logger?.LogInformation("{Method}: Permissions have been obtained", nameof(TryRequestStorageReadPermissions));
                return true;
            }

            _logger?.LogWarning("{Method}: Read storage permissions not obtained", nameof(TryRequestStorageReadPermissions));

            await Shell.Current.DisplayAlert("Предупреждение",
                     "Права на чтение файлов не были получены", "OK");

            return false;
        }

        private bool CheckPermissionsOnGranted(PermissionStatus permissionStatus)
        {
            return permissionStatus != PermissionStatus.Denied
               && permissionStatus != PermissionStatus.Disabled
               && permissionStatus != PermissionStatus.Unknown;
        }

        #endregion
    }
}

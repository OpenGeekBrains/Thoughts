using System.Windows;
using System.Windows.Input;

using Thoughts.UI.WPF.Infrastructure.Commands.Base;
using Thoughts.UI.WPF.ViewModels.Base;

namespace Thoughts.UI.WPF.ViewModels;

/// <summary>
/// ViewModel главного окна
/// </summary>
public class MainWindowViewModel:ViewModel
{
    private string _title;
    public string Title { get => _title; set => Set(ref _title, value); }

    public MainWindowViewModel()
    {
        Title = "Thoughts";
    }

    private ICommand? _ShutdownCommand;

    public ICommand ShutdownCommand => _ShutdownCommand ??= new RelayCommand(e => Application.Current.Shutdown());
}

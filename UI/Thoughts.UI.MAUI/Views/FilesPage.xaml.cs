using Thoughts.UI.MAUI.ViewModels;

namespace Thoughts.UI.MAUI.Views;

public partial class FilesPage : ContentPage
{
    public FilesPage(FilesViewModel fileViewModel)
	{
		InitializeComponent();
        BindingContext = fileViewModel;
    }
}
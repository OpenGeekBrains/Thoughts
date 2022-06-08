using System.Windows.Input;

namespace Thoughts.UI.WPF;

public partial class MainWindow
{
    public MainWindow() => InitializeComponent();

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            DragMove();
    }
}

using System.Windows;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace Thoughts.UI.WPF.Infrastructure.Behaviors;

public class DragWindow : Behavior<Window>
{
    protected override void OnAttached()
    {
        AssociatedObject.MouseDown += OnMouseDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseDown -= OnMouseDown;
    }

    private void OnMouseDown(object Sender, MouseButtonEventArgs E)
    {
        if(E.LeftButton == MouseButtonState.Pressed)
            AssociatedObject.DragMove();
    }
}

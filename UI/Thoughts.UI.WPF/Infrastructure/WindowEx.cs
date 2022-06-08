using System.Windows;
using System.Windows.Input;

namespace Thoughts.UI.WPF.Infrastructure;

public class WindowEx
{
    #region Attached property DragWindow : bool - Перемещение окна за любое место

    /// <summary>Перемещение окна за любое место</summary>
    [AttachedPropertyBrowsableForType(typeof(Window))]
    public static void SetDragWindow(DependencyObject d, bool value) => d.SetValue(DragWindowProperty, value);

    /// <summary>Перемещение окна за любое место</summary>
    public static bool GetDragWindow(DependencyObject d) => (bool)d.GetValue(DragWindowProperty);

    /// <summary>Перемещение окна за любое место</summary>
    public static readonly DependencyProperty DragWindowProperty =
        DependencyProperty.RegisterAttached(
            "DragWindow",
            typeof(bool),
            typeof(WindowEx),
            new PropertyMetadata(default(bool), OnDragWindowChanged));

    private static void OnDragWindowChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
    {
        var do_drag_move = (bool)E.NewValue;
        var window = (Window)D;

        if (do_drag_move)
            window.MouseDown += OnDragWindowMouseDown;
        else
            window.MouseDown -= OnDragWindowMouseDown;
    }

    private static void OnDragWindowMouseDown(object Sender, MouseButtonEventArgs E)
    {
        if(E.LeftButton == MouseButtonState.Pressed)
            ((Window)Sender).DragMove();
    }

    #endregion
}

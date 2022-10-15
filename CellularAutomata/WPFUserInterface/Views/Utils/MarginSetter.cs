using System.Windows;
using System.Windows.Controls;

namespace WPFUserInterface.Views.Utils;

public class MarginSetter
{
    public static Thickness GetMargin(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(MarginProperty);
    }

    public static void SetMargin(DependencyObject obj, Thickness value)
    {
        obj.SetValue(MarginProperty, value);
    }

    public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached(
        "Margin", typeof(Thickness), typeof(MarginSetter), new UIPropertyMetadata(new Thickness(), MarginChangedCallback));

    public static void MarginChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (sender is Panel panel)
            panel.Loaded += new RoutedEventHandler(PanelLoaded);
    }

    public static void PanelLoaded(object sender, RoutedEventArgs eventArgs)
    {
        var panel = sender as Panel;
        foreach (UIElement child in panel.Children)
        {
            if (child is FrameworkElement element)
                element.Margin = MarginSetter.GetMargin(panel);
        }
    }
}
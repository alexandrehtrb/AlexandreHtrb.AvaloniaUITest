using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AlexandreHtrb.AvaloniaUITest;

public partial class UITestsPrepareWindow : Window
{
    public UITestsPrepareWindow() => AvaloniaXamlLoader.Load(this);

    public UITestsPrepareWindow(UITestsPrepareWindowViewModel vm)
    {
        AvaloniaXamlLoader.Load(this);
        DataContext = vm;
    }

    private void RunTests(object? sender, RoutedEventArgs e)
    {
        Close();
        ((UITestsPrepareWindowViewModel)DataContext!).RunTests();
    }
}
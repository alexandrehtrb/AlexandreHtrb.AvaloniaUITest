using Avalonia.Controls;
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
}
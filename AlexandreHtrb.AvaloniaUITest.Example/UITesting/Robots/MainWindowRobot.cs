using Avalonia.Controls;

namespace AlexandreHtrb.AvaloniaUITest.Example.UITesting.Robots;

public sealed class MainWindowRobot : BaseRobot
{
    public MainWindowRobot(Control rootView) : base(rootView) { }

    internal TreeView Tree => GetChildView<TreeView>("mainWindowTree")!;
    internal TextBlock SelectedItemName => GetChildView<TextBlock>("tbSelectedItemName")!;
    internal TextBox NewChildName => GetChildView<TextBox>("tbNewChildName")!;
    internal Button DeleteSelectedItems => GetChildView<Button>("btDeleteSelectedItems")!;
    internal Button AddChild => GetChildView<Button>("btAddChild")!;
}
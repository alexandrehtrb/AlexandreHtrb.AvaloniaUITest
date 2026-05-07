using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;

namespace AlexandreHtrb.AvaloniaUITest;

public static class UITestActions
{
    internal static TimeSpan WaitingTimeAfterActions;

    public static Task WaitAfterActionAsync() => Task.Delay(WaitingTimeAfterActions);

    public static TreeViewItem? GetTreeViewItemViewAtIndex(this TreeView parentView, int index) =>
        (TreeViewItem?)parentView.GetLogicalChildren().OfType<TreeViewItem>().ElementAt(index);

    public static TreeViewItem? GetTreeViewItemViewAtIndex(this TreeViewItem parentView, int index) =>
        (TreeViewItem?)parentView.GetLogicalChildren().OfType<TreeViewItem>().ElementAt(index);

    public static IVM? GetItemAtIndex<IVM>(this TreeView parentView, int index) =>
        (IVM?)parentView.Items[index];

    public static IVM? GetItemAtIndex<IVM>(this TreeViewItem parentView, int index) =>
        (IVM?)parentView.Items[index];

    public static async Task ClickOn(this Button control)
    {
        control.Command?.Execute(null);
        await WaitAfterActionAsync();
    }

    public static async Task ClickOn(this MenuItem control)
    {
        // if it is a parent menu item (drawer), then just open
        if (control.Items.Count > 0)
        {
            control.Open();
        }
        else
        {
            RoutedEventArgs args = new(MenuItem.ClickEvent);
            control.RaiseEvent(args);
        }
        await WaitAfterActionAsync();
    }

    public static async Task ClickOn(this CheckBox cb)
    {
        cb.IsChecked = !cb.IsChecked;
        await WaitAfterActionAsync();
    }

    public static async Task Select(this ComboBox cb, string item)
    {
        cb.IsDropDownOpen = true;
        cb.SelectedIndex = cb.Items.IndexOf(
            cb.Items.First(x => x is string s && s == item));
        cb.IsDropDownOpen = false;
        await WaitAfterActionAsync();
    }

    public static async Task Select(this ComboBox cb, ComboBoxItem item)
    {
        cb.IsDropDownOpen = true;
        cb.SelectedIndex = cb.GetLogicalChildren().ToList().IndexOf(item);
        cb.IsDropDownOpen = false;
        await WaitAfterActionAsync();
    }

    public static async Task Select(this AutoCompleteBox acb, string? item)
    {
        acb.SelectedItem = item;
        await WaitAfterActionAsync();
    }

    public static async Task Select(this TabControl tc, TabItem ti)
    {
        tc.SelectedItem = ti;
        await WaitAfterActionAsync();
    }

    internal static async Task Select<IVM>(this TreeView tree, IVM itemViewModel)
    {
        tree.SelectedItem = itemViewModel;
        await WaitAfterActionAsync();
    }

    internal static async Task SelectMultiple<IVM>(this TreeView tree, params IVM[] itemsViewModels)
    {
        tree.SelectedItem = null;
        tree.SelectedItems.Clear();
        foreach (var item in itemsViewModels)
        {
            tree.SelectedItems.Add(item);
        }
        await WaitAfterActionAsync();
    }

    public static async Task ClearText(this TextBox txtBox)
    {
        txtBox.Clear();
        await WaitAfterActionAsync();
    }

    public static async Task TypeText(this TextBox control, string txt)
    {
        foreach (char c in txt)
        {
            TextInputEventArgs args = new();
            args.RoutedEvent = InputElement.TextInputEvent;
            args.Text = c.ToString();
            control.RaiseEvent(args);
        }
        await WaitAfterActionAsync();
    }

    public static async Task ClearAndTypeText(this TextBox txtBox, string newTxt)
    {
        await txtBox.ClearText();
        await txtBox.TypeText(newTxt);
    }

    public static async Task SetValue(this NumericUpDown nud, int val)
    {
        nud.Value = val;
        await WaitAfterActionAsync();
    }

    public static async Task PressKey(this Control control, Key key, KeyModifiers keyModifiers = KeyModifiers.None)
    {
        KeyEventArgs args = new() { Key = key, KeyModifiers = keyModifiers };
        args.RoutedEvent = InputElement.KeyDownEvent;
        control.RaiseEvent(args);
        args.RoutedEvent = InputElement.KeyUpEvent;
        control.RaiseEvent(args);
        await WaitAfterActionAsync();
    }
}
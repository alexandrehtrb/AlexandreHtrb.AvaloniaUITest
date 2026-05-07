using Avalonia.Controls;
using Avalonia.Media;

namespace AlexandreHtrb.AvaloniaUITest;

public static class UITestAssertions
{
    public static void AssertCondition(bool condition, string? msg = null)
    {
        if (condition == false)
        {
            throw msg is null ? new UITestException() : new UITestException(msg);
        }
    }

    public static void AssertIsHidden(this Control control) => AssertCondition((control.IsMeasureValid && control.IsEffectivelyVisible) == false, "Control should be hidden.");
    public static void AssertIsVisible(this Control control) => AssertCondition((control.IsMeasureValid && control.IsEffectivelyVisible) == true, "Control should be visible.");
    public static void AssertHasStyleClass(this Control control, string className) => AssertCondition(control.Classes.Contains(className), $"Control should have style class: '{className}'. Classes found: {string.Join(',', control.Classes)}");
    public static void AssertDoesntHaveStyleClass(this Control control, string className) => AssertCondition(control.Classes.Contains(className) == false, $"Control shouldn't have style class: '{className}'. Classes found: {string.Join(',', control.Classes)}");
    public static void AssertHasText(this TextBlock txtBlock, string? txt) => AssertCondition(txtBlock.Text == txt, $"Text should be: '{txt}', reality: '{txtBlock.Text}'.");
    public static void AssertHasText(this AutoCompleteBox txtBox, string? txt) => AssertCondition(txtBox.Text == txt, $"Text should be: '{txt}', reality: '{txtBox.Text}'.");
    public static void AssertHasText(this TextBox txtBox, string? txt) => AssertCondition(txtBox.Text == txt, $"Text should be: '{txt}', reality: '{txtBox.Text}'.");
    public static void AssertHasText(this Button button, string txt) => AssertCondition(((string)button.Content!) == txt, "Text should be: '" + txt + "'.");
    public static void AssertHasText(this ComboBox cb, string txt) => AssertCondition(cb.SelectedItem is string s && s == txt, "Text should be: '" + txt + "'.");
    public static void AssertHasText(this ComboBoxItem cbItem, string txt) => AssertCondition(((string)cbItem.Content!) == txt, "Text should be: '" + txt + "'.");
    public static void AssertHasText(this MenuItem menuItem, string txt) => AssertCondition(((string)menuItem.Header!) == txt, "Text should be: '" + txt + "'.");
    public static void AssertHasText(this TreeViewItem tvi, string txt) => AssertCondition(((string)tvi.Header!) == txt, "Text should be: '" + txt + "'.");
    public static void AssertHasText(this CheckBox cb, string txt) => AssertCondition((string)cb.Content! == txt, "Text should be: '" + txt + "'.");
    public static void AssertHasPlaceholderText(this TextBox txtBox, string txt) => AssertCondition(txtBox.PlaceholderText == txt, $"Text should be: '{txt}', reality: '{txtBox.Text}'.");
    public static void AssertContainsText(this TextBox txtBox, string txt) => AssertCondition(txtBox.Text?.Contains(txt) == true, "Text should contain: '" + txt + "'.");
    public static void AssertContainsText(this TextBlock txtBlock, string txt) => AssertCondition(txtBlock.Text?.Contains(txt) == true, "Text should contain: '" + txt + "'.");
    public static void AssertHasIconVisible(this MenuItem menuItem) => AssertCondition(((Image)menuItem.Icon!).IsVisible == true, "Control's icon should be visible.");
    public static void AssertHasIconHidden(this MenuItem menuItem) => AssertCondition(((Image)menuItem.Icon!).IsVisible == false, "Control's icon shouldn't be visible.");
    public static void AssertSelection(this ComboBox cb, ComboBoxItem cbi) => AssertCondition(cb.SelectedItem == cbi, "ComboBox selection doesn't match expectation.");
    public static void AssertSelection(this ComboBox cb, string txt) => AssertHasText(cb, txt);
    public static void AssertNumericValue(this NumericUpDown nud, int val) => AssertCondition(nud.Value is decimal d && ((int)d) == val, $"NumericUpDown value doesn't match expectation: '{val}', actual: '{nud.Value}'");
    public static void AssertBackgroundColor(this Panel panel, string hexColorWithPoundSign) => AssertCondition(panel.Background is SolidColorBrush scb && ToHexString(scb.Color) == hexColorWithPoundSign, "Panel background color doesn't match expectation: " + hexColorWithPoundSign);
    public static void AssertIsChecked(this CheckBox cb) => AssertCondition(cb.IsChecked == true, "CheckBox should be checked.");
    public static void AssertIsNotChecked(this CheckBox cb) => AssertCondition(cb.IsChecked == false, "CheckBox shouldn't be checked.");

    private static string ToHexString(Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";
}
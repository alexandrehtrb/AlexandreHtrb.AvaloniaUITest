using AlexandreHtrb.AvaloniaUITest.Example.UITesting.Robots;
using AlexandreHtrb.AvaloniaUITest.Example.ViewModels;
using AlexandreHtrb.AvaloniaUITest.Example.Views;
using Avalonia.Controls;
using static AlexandreHtrb.AvaloniaUITest.UITestAssertions;

namespace AlexandreHtrb.AvaloniaUITest.Example.UITesting.Tests;

public sealed class AddChildUITest : UITest
{
    private MainWindowRobot Robot { get; }

    public AddChildUITest()
    {
        var content = MainWindow.Instance!.Content;
        Robot = new((Control)content!);
    }

    public override async Task RunAsync(CancellationToken cancellationToken)
    {
        AppendToLog("Starting my test!");

        Robot.Tree.AssertIsVisible();
        Robot.DeleteSelectedItems.AssertIsVisible();
        Robot.SelectedItemName.AssertIsHidden();
        Robot.SelectedItemName.AssertHasText(null);
        Robot.NewChildName.AssertIsHidden();
        Robot.AddChild.AssertIsHidden();

        var rootItem = Robot.Tree.GetItemAtIndex<TreeItemViewModel>(0)!;
        var rootItemTvi = Robot.Tree.GetTreeViewItemViewAtIndex(0)!;
        await Robot.Tree.Select(rootItem);
        await Wait(1);

        // The cancellationToken can be used as a stopping point
        // in a long test, so it can be interrupted.
        if (cancellationToken.IsCancellationRequested)
            return;

        Robot.SelectedItemName.AssertIsVisible();
        Robot.SelectedItemName.AssertHasText("Root (click me)");
        Robot.NewChildName.AssertIsVisible();
        Robot.NewChildName.AssertHasPlaceholderText("New child name");
        Robot.NewChildName.AssertHasText(string.Empty);
        Robot.AddChild.AssertIsVisible();
        await Robot.NewChildName.TypeText("Child1");
        await Robot.AddChild.ClickOn();
        await Wait(1);
        AssertCondition(rootItem.IsExpanded == true, "Parent should be expanded after adding a child");

        var child1Item = rootItemTvi.GetItemAtIndex<TreeItemViewModel>(0)!;
        AssertCondition(Robot.Tree.SelectedItem == child1Item, "Newly created child should be tree's selected item");

        Robot.SelectedItemName.AssertIsVisible();
        Robot.SelectedItemName.AssertHasText("Child1");
        Robot.NewChildName.AssertIsVisible();
        Robot.NewChildName.AssertHasPlaceholderText("New child name");
        Robot.NewChildName.AssertHasText(string.Empty);
        Robot.AddChild.AssertIsVisible();
    }

    protected override void Teardown()
    {
        base.Teardown();
        MainWindowViewModel.Instance!.TreeItems.Clear();
        MainWindowViewModel.Instance!.TreeItems.Add(new(MainWindowViewModel.Instance!.TreeItems, "Root (click me)"));
    }
}
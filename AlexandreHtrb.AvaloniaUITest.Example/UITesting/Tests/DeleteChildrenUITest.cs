using AlexandreHtrb.AvaloniaUITest.Example.UITesting.Robots;
using AlexandreHtrb.AvaloniaUITest.Example.ViewModels;
using AlexandreHtrb.AvaloniaUITest.Example.Views;
using Avalonia.Controls;
using static AlexandreHtrb.AvaloniaUITest.UITestAssertions;

namespace AlexandreHtrb.AvaloniaUITest.Example.UITesting.Tests;

public sealed class DeleteChildrenUITest : UITest
{
    private MainWindowRobot Robot { get; }

    public DeleteChildrenUITest()
    {
        var content = MainWindow.Instance!.Content;
        Robot = new((Control)content!);
    }

    public override async Task RunAsync(CancellationToken cancellationToken)
    {
        AppendToLog("Starting my test!");

        var rootItem = Robot.Tree.GetItemAtIndex<TreeItemViewModel>(0)!;
        await Robot.Tree.Select(rootItem);
        await Wait(1);

        await Robot.NewChildName.ClearAndTypeText("Child1");
        await Robot.AddChild.ClickOn();

        await Robot.NewChildName.ClearAndTypeText("Child1_1");
        await Robot.AddChild.ClickOn();

        await Robot.Tree.Select(rootItem);

        await Robot.NewChildName.ClearAndTypeText("Child2");
        await Robot.AddChild.ClickOn();

        await Robot.NewChildName.ClearAndTypeText("Child2_1");
        await Robot.AddChild.ClickOn();

        await Robot.Tree.Select(rootItem.Items[1]); // selecting Child2

        await Robot.NewChildName.ClearAndTypeText("Child2_2");
        await Robot.AddChild.ClickOn();

        var rootTvi = Robot.Tree.GetTreeViewItemViewAtIndex(0)!;
        AssertCondition(rootTvi.Items.Count == 2);

        var child1Tvi = rootTvi.GetTreeViewItemViewAtIndex(0)!;
        AssertCondition(child1Tvi.Items.Count == 1);

        var child1_1Tvi = child1Tvi.GetTreeViewItemViewAtIndex(0)!;
        AssertCondition(child1_1Tvi.Items.Count == 0);

        var child2Tvi = rootTvi.GetTreeViewItemViewAtIndex(1)!;
        AssertCondition(child2Tvi.Items.Count == 2);

        var child2_1Tvi = child2Tvi.GetTreeViewItemViewAtIndex(0)!;
        AssertCondition(child2_1Tvi.Items.Count == 0);

        var child2_2Tvi = child2Tvi.GetTreeViewItemViewAtIndex(1)!;
        AssertCondition(child2_2Tvi.Items.Count == 0);

        // The cancellationToken can be used as a stopping point
        // in a long test, so it can be interrupted.
        if (cancellationToken.IsCancellationRequested)
            return;

        // The tree should be:
        //
        // Root
        // -> Child1
        //    -> Child1_1
        // -> Child2
        //    -> Child2_1
        //    -> Child2_2
        //
        // Let's delete: Child2_2 and Child1.
        // Deleting Child1 should remove Child1_1 too.

        var child1Item = rootItem.Items[0];
        var child2_2Item = rootItem.Items[1].Items[1];
        await Wait(1);
        await Robot.Tree.SelectMultiple(child1Item, child2_2Item);
        await Robot.DeleteSelectedItems.ClickOn();

        AssertCondition(rootTvi.Items.Count == 1);
        AssertCondition(rootTvi.GetItemAtIndex<TreeItemViewModel>(0)!.Name == "Child2");

        child2Tvi = rootTvi.GetTreeViewItemViewAtIndex(0)!;
        AssertCondition(child2Tvi.Items.Count == 1);
        AssertCondition(child2Tvi.GetItemAtIndex<TreeItemViewModel>(0)!.Name == "Child2_1");
    }

    protected override void Teardown()
    {
        base.Teardown();
        MainWindowViewModel.Instance!.TreeItems.Clear();
        MainWindowViewModel.Instance!.TreeItems.Add(new(MainWindowViewModel.Instance!.TreeItems, "Root (click me)"));
    }
}
using System.Collections.ObjectModel;
using AlexandreHtrb.AvaloniaUITest.Example.UITesting.Tests;
using AlexandreHtrb.AvaloniaUITest.Example.Views;
using MsBox.Avalonia.Enums;

namespace AlexandreHtrb.AvaloniaUITest.Example.ViewModels;

public class MainWindowViewModel : UITestBaseViewModel
{
#nullable disable warnings
    public static MainWindowViewModel Instance;
#nullable restore warnings

    private static readonly UITestsPrepareWindowViewModel uiTestsVm = new(
        defaultActionWaitingTimeInMs: 20,
        uiTests: [
            new AddChildUITest(),
            new DeleteChildrenUITest()
        ],
        beforeStartTestsCallback: () => Instance!.IsRunningTests = true,
        uiTestsFinishedCallback: (resultsLog) =>
        {
            Instance!.IsRunningTests = false;
            Dialogs.ShowDialog(
                title: "UI tests results",
                message: resultsLog,
                buttons: ButtonEnum.Ok);
        });

    public ObservableCollection<TreeItemViewModel> TreeItems { get; }
    
    public ObservableCollection<TreeItemViewModel> TreeSelectedItems { get; }

    public TreeItemViewModel? TreeSelectedItem
    {
        get;
        set
        {
            ChangeProperty(ref field, value);
            HasTreeSelectedItem = value != null;
        }
    }

    public bool HasTreeSelectedItem { get; set => ChangeProperty(ref field, value); }

    public bool HasTreeSelectedItems { get; set => ChangeProperty(ref field, value); }

    public UITestRelayCommand DeleteSelectedItemsCmd { get; }

    private string runOrStopTestsTextField = string.Empty;
    public string RunOrStopTestsText
    {
        get => this.runOrStopTestsTextField;
        set => ChangeProperty(ref this.runOrStopTestsTextField!, value);
    }

    private bool isRunningTestsField;
    public bool IsRunningTests
    {
        get => this.isRunningTestsField;
        set
        {
            ChangeProperty(ref this.isRunningTestsField, value);
            RunOrStopTestsText = value ? "Press F8 to stop tests" : "Press F7 to run tests";
        }
    }

    public UITestRelayCommand OpenUITestsDialogCmd { get; }

    public UITestRelayCommand StopUITestsCmd { get; }
    
    public MainWindowViewModel()
    {
        Instance = this;
        TreeItems = new();
        TreeItems.Add(new(TreeItems, "Root (click me)"));
        TreeSelectedItems = new();
        TreeSelectedItem = null;
        IsRunningTests = false;
        DeleteSelectedItemsCmd = new(DeleteSelectedItems);
        OpenUITestsDialogCmd = new(OpenUITestsDialog);
        StopUITestsCmd = new(StopUITests);
    }

    private void DeleteSelectedItems() =>
        TreeSelectedItems.ToList().ForEach(x => x.DeleteThis());

    private void OpenUITestsDialog()
    {
        UITestsPrepareWindow uiTestsPrepareWindow = new(uiTestsVm);
        uiTestsPrepareWindow.Show(MainWindow.Instance!);
    }

    private void StopUITests() => uiTestsVm.StopTests();

}
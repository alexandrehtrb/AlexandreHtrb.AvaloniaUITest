using System.Collections.ObjectModel;
using AlexandreHtrb.AvaloniaUITest;
using Avalonia.Threading;

namespace AlexandreHtrb.AvaloniaUITest;

public class UITestViewModel : UITestBaseViewModel
{
    private bool includeField;
    public bool Include
    {
        get => this.includeField;
        set => ChangeProperty(ref this.includeField, value);
    }

    private string? nameField;
    public string? Name
    {
        get => this.nameField;
        set => ChangeProperty(ref this.nameField, value);
    }

    public UITest Test { get; }

    public UITestViewModel(string name, UITest test)
    {
        Include = true;
        Name = name;
        Test = test;
    }
}

public class UITestsPrepareWindowViewModel : UITestBaseViewModel
{
    private readonly Action<string> uiTestsFinishedCallback;

    private int actionsWaitingtimeInMsField;
    public int ActionsWaitingTimeInMs
    {
        get => this.actionsWaitingtimeInMsField;
        set => ChangeProperty(ref this.actionsWaitingtimeInMsField, value);
    }

    public ObservableCollection<UITestViewModel> Tests { get; }

    public UITestRelayCommand SelectAllTestsCmd { get; }

    public UITestRelayCommand DeselectAllTestsCmd { get; }

    public UITestsPrepareWindowViewModel(int defaultActionWaitingTimeInMs, UITest[] uiTests, Action<string> uiTestsFinishedCallback)
    {
        this.uiTestsFinishedCallback = uiTestsFinishedCallback;
        ActionsWaitingTimeInMs = defaultActionWaitingTimeInMs;
        Tests = new(uiTests.Select(t => new UITestViewModel(t.TestName, t)));
        SelectAllTestsCmd = new(SelectAllTests);
        DeselectAllTestsCmd = new(DeselectAllTests);
    }

    private void SelectAllTests()
    {
        foreach (var test in Tests)
        {
            test.Include = true;
        }
    }

    private void DeselectAllTests()
    {
        foreach (var test in Tests)
        {
            test.Include = false;
        }
    }

    internal void RunTests() => Dispatcher.UIThread.Post(async () => await RunTestsAsync());

    protected async Task RunTestsAsync()
    {
        var waitingTimeBetweenActions = TimeSpan.FromMilliseconds(ActionsWaitingTimeInMs);
        var tests = Tests.Where(t => t.Include).Select(t => t.Test).ToArray();

        string resultsLog = await UITestsRunner.RunTestsAsync(waitingTimeBetweenActions, tests);

        this.uiTestsFinishedCallback(resultsLog);
    }
}
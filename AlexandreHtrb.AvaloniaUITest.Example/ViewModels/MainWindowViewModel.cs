using AlexandreHtrb.AvaloniaUITest.Example.UITesting.Tests;
using AlexandreHtrb.AvaloniaUITest.Example.Views;
using MsBox.Avalonia.Enums;

namespace AlexandreHtrb.AvaloniaUITest.Example.ViewModels;

public class MainWindowViewModel : UITestBaseViewModel
{
    private string greetingField;
    public string Greeting
    {
        get => this.greetingField;
        set => ChangeProperty(ref this.greetingField!, value);
    }

    private int clickCounter = 0;

    private string clickedCounterMessageField;
    public string ClickedCounterMessage
    {
        get => this.clickedCounterMessageField;
        set => ChangeProperty(ref this.clickedCounterMessageField!, value);
    }

    public UITestRelayCommand ClickCmd { get; }

    public UITestRelayCommand ResetCmd { get; }

    public UITestRelayCommand RunUITestsCmd { get; }

#nullable disable warnings
    public MainWindowViewModel()
    {
#nullable restore warnings
#if DEBUG || UI_TESTS_ENABLED
        Greeting = "Press F7 to run UI tests";
#else
        Greeting = "Welcome to Avalonia!";
#endif
        ClickedCounterMessage = "Clicked 0 times";
        ClickCmd = new(Click);
        ResetCmd = new(Reset);
        RunUITestsCmd = new(RunUITests);
    }

    private void Click()
    {
        this.clickCounter++;
        ClickedCounterMessage = $"Clicked {this.clickCounter} times";
    }

    private void Reset()
    {
        this.clickCounter = 0;
        ClickedCounterMessage = $"Clicked {this.clickCounter} times";
    }

#if DEBUG || UI_TESTS_ENABLED
    private void RunUITests()
    {
        UITestsPrepareWindowViewModel vm = new(
            defaultActionWaitingTimeInMs: 20,
            uiTests: [
                new MainWindowUITest()
            ],
            uiTestsFinishedCallback: (resultsLog) =>
            {
                Dialogs.ShowDialog(
                    title: "UI tests results",
                    message: resultsLog,
                    buttons: ButtonEnum.Ok);
            });
        UITestsPrepareWindow uiTestsPrepareWindow = new(vm);
        uiTestsPrepareWindow.Show(MainWindow.Instance!);
    }
#else
    private void RunUITests() {};
#endif

}

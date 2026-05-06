using AlexandreHtrb.AvaloniaUITest.Example.UITesting.Robots;
using AlexandreHtrb.AvaloniaUITest.Example.Views;
using Avalonia.Controls;

namespace AlexandreHtrb.AvaloniaUITest.Example.UITesting.Tests;

public sealed class MainWindowUITest : UITest
{
    private MainWindowRobot Robot { get; }

    public MainWindowUITest()
    {
        var content = MainWindow.Instance!.Content;
        Robot = new((Control)content!);
    }

    public override async Task RunAsync(CancellationToken cancellationToken)
    {
        AppendToLog("Starting my test!");

        Robot.GreetingMsg.AssertIsVisible();
        Robot.CounterMsg.AssertIsVisible();
        Robot.BtClick.AssertIsVisible();
        Robot.BtReset.AssertIsVisible();
        await Robot.BtReset.ClickOn();

        if (cancellationToken.IsCancellationRequested)
            return;

        Robot.GreetingMsg.AssertHasText("Press F7 to run UI tests");
        Robot.CounterMsg.AssertHasText("Clicked 0 times");
        Robot.BtClick.AssertHasText("Click me");

        if (cancellationToken.IsCancellationRequested)
            return;

        await Robot.BtClick.ClickOn();
        Robot.CounterMsg.AssertHasText("Clicked 1 times");

        if (cancellationToken.IsCancellationRequested)
            return;

        await Robot.BtClick.ClickOn();
        Robot.CounterMsg.AssertHasText("Clicked 2 times");

        if (cancellationToken.IsCancellationRequested)
            return;

        await Robot.BtReset.ClickOn();
        await Wait(1);
        Robot.CounterMsg.AssertHasText("Clicked 0 times");
    }
}
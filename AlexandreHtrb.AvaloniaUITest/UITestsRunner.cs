using System.Text;

namespace AlexandreHtrb.AvaloniaUITest;

public static class UITestsRunner
{
    public static async Task<string> RunTestsAsync(TimeSpan waitingTimeBetweenActions, CancellationToken cancellationToken, params UITest[] tests)
    {
        UITestActions.WaitingTimeAfterActions = waitingTimeBetweenActions;

        static TimeSpan SumTotalTime(IEnumerable<UITest> ts)
        {
            var totalTime = TimeSpan.Zero;
            foreach (var t in ts)
            {
                totalTime += t.ElapsedTime;
            }
            return totalTime;
        }

        StringBuilder allTestsLogsAppender = new();
        foreach (var test in tests)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                allTestsLogsAppender.AppendLine("---- TESTS EXECUTION STOPPED BY USER ----");
                break;
            }

            await RunTestAsync(allTestsLogsAppender, test, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                allTestsLogsAppender.AppendLine("---- TESTS EXECUTION STOPPED BY USER ----");
                break;
            }
        }
        var totalTime = SumTotalTime(tests);
        string fmtTime = @"hh'h'mm'm'ss's'";
        allTestsLogsAppender.AppendLine("TOTAL TIME: " + totalTime.ToString(fmtTime));
        return allTestsLogsAppender.ToString();
    }

    private static async Task RunTestAsync(StringBuilder allTestsLogsAppender, UITest test, CancellationToken cancellationToken)
    {
        bool wasTestStopped = false;
        Exception? possibleException = null;
        try
        {
            test.Start();
            await test.RunAsync(cancellationToken);
            wasTestStopped = cancellationToken.IsCancellationRequested;
        }
        catch (Exception ex)
        {
            test.Successful = false;
            possibleException = ex;
            if (ex is TaskCanceledException || ex is OperationCanceledException)
            {
                wasTestStopped = true;
            }            
        }
        finally
        {
            test.Finish();
            if (!string.IsNullOrWhiteSpace(test.Log))
            {
                allTestsLogsAppender.Append(test.Log);
                test.ResetInternalLog();
            }
            if (possibleException != null)
            {
                allTestsLogsAppender.AppendLine(possibleException.ToString());
            }
            allTestsLogsAppender.AppendLine($"{test.TestName}: {(wasTestStopped ? "STOPPED" : test.Successful == true ? "SUCCESS" : "FAILED")} {test.TotalElapsedSeconds}s");
        }
    }
}
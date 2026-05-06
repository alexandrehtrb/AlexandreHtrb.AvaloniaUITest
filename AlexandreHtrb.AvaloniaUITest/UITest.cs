using System.Diagnostics;
using System.Text;

namespace AlexandreHtrb.AvaloniaUITest;

public abstract class UITest
{
    public string TestName => this.GetType().Name;

    public string Log => this.logAppender.ToString();

    public bool? Successful { get; internal set; }

    private readonly StringBuilder logAppender;

    private readonly Stopwatch stopwatch;

    public int TotalElapsedSeconds => (int)this.stopwatch.Elapsed.TotalSeconds;

    public TimeSpan ElapsedTime => this.stopwatch.Elapsed;

    public UITest()
    {
        this.logAppender = new();
        this.stopwatch = new();
    }

    public void Start()
    {
        Successful = null;
        this.stopwatch.Start();
    }

    public void Finish()
    {
        Successful = Successful is null;
        this.stopwatch.Stop();
        Teardown();
    }

    protected virtual void Teardown()
    {
    }

    internal void ResetInternalLog() => this.logAppender.Clear();

    public Task Wait(double seconds) => Task.Delay((int)(seconds * 1000));

    protected void AppendToLog(string msg) => this.logAppender.AppendLine(msg);

    public abstract Task RunAsync(CancellationToken cancellationToken);
}

public sealed class UITestException : Exception
{
    public UITestException() : base() { }
    public UITestException(string msg) : base(msg) { }
}
using LW03_HW.Core;
using LW03_HW.Core.Interfaces;
using LW03_HW.Core.Reporters;
using Xunit;

namespace LW03_HW.Tests;
public class FakeObserver : IObserver
{
    public List<AnalysisResult> ReceivedFails { get; } = new List<AnalysisResult>();

    public void OnFail(AnalysisResult result)
    {
        ReceivedFails.Add(result);
    }
}

public class ConsoleReporterTests
{
    [Fact]
    public void Report_WithFailResult_NotifiesObserver()
    {
        var reporter = new ConsoleReporter();
        var observer = new FakeObserver();
        reporter.Subscribe(observer);

        var results = new List<AnalysisResult>
        {
            new AnalysisResult { RuleName = "Coverage", Passed = false, Details = "72% < 80%" }
        };

        reporter.Report(results);

        Assert.Single(observer.ReceivedFails);
        Assert.Equal("Coverage", observer.ReceivedFails[0].RuleName);
    }

    [Fact]
    public void Report_WithPassResult_DoesNotNotifyObserver()
    {
        var reporter = new ConsoleReporter();
        var observer = new FakeObserver();
        reporter.Subscribe(observer);

        var results = new List<AnalysisResult>
        {
            new AnalysisResult { RuleName = "Coverage", Passed = true, Details = "85% >= 80%" }
        };

        reporter.Report(results);

        Assert.Empty(observer.ReceivedFails);
    }

    [Fact]
    public void Report_AfterUnsubscribe_ObserverNotNotified()
    {
        var reporter = new ConsoleReporter();
        var observer = new FakeObserver();
        reporter.Subscribe(observer);
        reporter.Unsubscribe(observer);

        var results = new List<AnalysisResult>
        {
            new AnalysisResult { RuleName = "Coverage", Passed = false, Details = "fail" }
        };

        reporter.Report(results);

        Assert.Empty(observer.ReceivedFails);
    }

    [Fact]
    public void Report_MultipleObservers_AllNotified()
    {
        var reporter = new ConsoleReporter();
        var obs1 = new FakeObserver();
        var obs2 = new FakeObserver();
        reporter.Subscribe(obs1);
        reporter.Subscribe(obs2);

        var results = new List<AnalysisResult>
        {
            new AnalysisResult { RuleName = "Duplication", Passed = false, Details = "fail" }
        };

        reporter.Report(results);

        Assert.Single(obs1.ReceivedFails);
        Assert.Single(obs2.ReceivedFails);
    }
}

public class JsonReporterTests
{
    [Fact]
    public void Report_WithFailResult_NotifiesObserver()
    {
        var reporter = new JsonReporter();
        var observer = new FakeObserver();
        reporter.Subscribe(observer);

        var results = new List<AnalysisResult>
        {
            new AnalysisResult { RuleName = "Coverage", Passed = false, Details = "fail" }
        };

        reporter.Report(results);

        Assert.Single(observer.ReceivedFails);
    }

    [Fact]
    public void Report_AfterUnsubscribe_ObserverNotNotified()
    {
        var reporter = new JsonReporter();
        var observer = new FakeObserver();
        reporter.Subscribe(observer);
        reporter.Unsubscribe(observer);

        var results = new List<AnalysisResult>
        {
            new AnalysisResult { RuleName = "Coverage", Passed = false, Details = "fail" }
        };

        reporter.Report(results);

        Assert.Empty(observer.ReceivedFails);
    }
}

public class ReporterFactoryTests
{
    private readonly ReporterFactory _factory = new ReporterFactory();

    [Fact]
    public void Create_ConsoleFormat_ReturnsConsoleReporter()
    {
        var reporter = _factory.Create("console");
        Assert.IsType<ConsoleReporter>(reporter);
    }

    [Fact]
    public void Create_JsonFormat_ReturnsJsonReporter()
    {
        var reporter = _factory.Create("json");
        Assert.IsType<JsonReporter>(reporter);
    }

    [Fact]
    public void Create_UnknownFormat_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _factory.Create("xml"));
    }
}

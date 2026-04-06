using LW03_HW.Core.Interfaces;
using System.Text.Json;

namespace LW03_HW.Core.Reporters;

public class JsonReporter : IReporter
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
    }
    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }
    public void Report(List<AnalysisResult> results)
    {
        foreach (var result in results)
        {
            if (!result.Passed)
            {
                foreach (var observer in _observers)
                {
                    observer.OnFail(result);
                }
            }
        }

        var jsonResults = results.Select(r => new
        {
            rule = r.RuleName,
            status = r.Passed ? "PASS" : "FAIL",
            details = r.Details
        });

        var output = new
        {
            qualityGate = results.All(r => r.Passed) ? "PASSED" : "FAILED",
            totalRules = results.Count,
            passCount = results.Count(r => r.Passed),
            failCount = results.Count(r => !r.Passed),
            results = jsonResults
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(output, options);
        Console.WriteLine(json);
    }
}

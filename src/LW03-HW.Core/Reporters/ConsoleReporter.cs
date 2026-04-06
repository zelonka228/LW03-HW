using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Reporters;


public class ConsoleReporter : IReporter
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

    /// <inheritdoc/>
    public void Report(List<AnalysisResult> results)
    {
        Console.WriteLine("========== Quality Gate Report ==========");

        foreach (var result in results)
        {
            string status = result.Passed ? "PASS" : "FAIL";
            Console.WriteLine($"[{status}] {result.RuleName}: {result.Details}");

            if (!result.Passed)
            {
                foreach (var observer in _observers)
                {
                    observer.OnFail(result);
                }
            }
        }

        int passCount = results.Count(r => r.Passed);
        int failCount = results.Count(r => !r.Passed);
        Console.WriteLine("=========================================");
        Console.WriteLine($"Total: {results.Count} rules | PASS: {passCount} | FAIL: {failCount}");
        Console.WriteLine(failCount == 0 ? "Quality Gate: PASSED" : "Quality Gate: FAILED");
    }
}

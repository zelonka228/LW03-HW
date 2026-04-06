
using System.Text.Json;
using QualityGate.Core.Interfaces;

namespace QualityGate.Core.Reporters;

public class JsonReporter : IReporter
{
    private readonly List<IObserver> _observers = new();

    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);

    public void Report(List<(string rule, bool result)> results)
    {
        var json = JsonSerializer.Serialize(results);
        Console.WriteLine(json);

        foreach (var r in results)
            if (!r.result)
                foreach (var obs in _observers)
                    obs.Update($"{r.rule} failed");
    }
}


using QualityGate.Core.Interfaces;

namespace QualityGate.Core.Reporters;

public class ConsoleReporter : IReporter
{
    private readonly List<IObserver> _observers = new();

    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);

    public void Report(List<(string rule, bool result)> results)
    {
        foreach (var r in results)
        {
            Console.WriteLine($"{r.rule}: {r.result}");
            if (!r.result)
            {
                foreach (var obs in _observers)
                    obs.Update($"{r.rule} failed");
            }
        }
    }
}


using QualityGate.Core.Interfaces;

namespace QualityGate.Core.Observers;

public class LoggerObserver : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"[Observer] {message}");
    }
}

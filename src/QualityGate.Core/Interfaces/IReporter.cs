
namespace QualityGate.Core.Interfaces;

public interface IReporter
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void Report(List<(string rule, bool result)> results);
}

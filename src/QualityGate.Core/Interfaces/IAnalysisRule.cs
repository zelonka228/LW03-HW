
namespace QualityGate.Core.Interfaces;

public interface IAnalysisRule
{
    string Name { get; }
    bool Evaluate(Dictionary<string, int> metrics);
}

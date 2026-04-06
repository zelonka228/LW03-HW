
using QualityGate.Core.Interfaces;

namespace QualityGate.Plugins;

public class DuplicationRule : IAnalysisRule
{
    public string Name => "Duplication";

    public bool Evaluate(Dictionary<string, int> metrics)
    {
        return metrics["duplication"] <= 10;
    }
}

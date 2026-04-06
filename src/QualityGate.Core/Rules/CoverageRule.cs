
using QualityGate.Core.Interfaces;

namespace QualityGate.Core.Rules;

public class CoverageRule : IAnalysisRule
{
    public string Name => "Coverage";

    public bool Evaluate(Dictionary<string, int> metrics)
    {
        return metrics["coverage"] >= 80;
    }
}

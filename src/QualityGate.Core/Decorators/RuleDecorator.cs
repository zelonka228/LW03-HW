
using QualityGate.Core.Interfaces;

namespace QualityGate.Core.Decorators;

public class RuleDecorator : IAnalysisRule
{
    private readonly IAnalysisRule _rule;

    public string Name => _rule.Name;

    public RuleDecorator(IAnalysisRule rule)
    {
        _rule = rule;
    }

    public bool Evaluate(Dictionary<string, int> metrics)
    {
        var result = _rule.Evaluate(metrics);
        Console.WriteLine($"[LOG] {Name}: {result}");
        return result;
    }
}

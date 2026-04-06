using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Rules;

public class RuleDecorator : IAnalysisRule
{
    private readonly IAnalysisRule _innerRule;

    public RuleDecorator(IAnalysisRule innerRule)
    {
        _innerRule = innerRule;
    }


    public string RuleName => _innerRule.RuleName;

    public AnalysisResult Check(Dictionary<string, double> metrics)
    {
        Console.WriteLine($"[LOG] Running rule: {_innerRule.RuleName}...");

        var result = _innerRule.Check(metrics);

        string status = result.Passed ? "PASS" : "FAIL";
        Console.WriteLine($"[LOG] Rule '{_innerRule.RuleName}' finished with: {status}");

        return result;
    }
}

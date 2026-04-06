using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Rules;

public class ComplexityRule : IAnalysisRule
{
    private readonly double _maxComplexity;

    public ComplexityRule(double maxComplexity = 10.0)
    {
        _maxComplexity = maxComplexity;
    }

    public string RuleName => "Complexity";

    public AnalysisResult Check(Dictionary<string, double> metrics)
    {
        if (!metrics.TryGetValue("complexity", out double value))
        {
            return new AnalysisResult
            {
                RuleName = RuleName,
                Passed = false,
                Details = "Metric 'complexity' not found."
            };
        }

        bool passed = value <= _maxComplexity;
        return new AnalysisResult
        {
            RuleName = RuleName,
            Passed = passed,
            Details = $"Complexity = {value} (required <= {_maxComplexity})"
        };
    }
}

using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Rules;

public class CoverageRule : IAnalysisRule
{
    private readonly double _minCoverage;

    public CoverageRule(double minCoverage = 80.0)
    {
        _minCoverage = minCoverage;
    }

    public string RuleName => "Coverage";

    public AnalysisResult Check(Dictionary<string, double> metrics)
    {
        if (!metrics.TryGetValue("coverage", out double value))
        {
            return new AnalysisResult
            {
                RuleName = RuleName,
                Passed = false,
                Details = "Metric 'coverage' not found."
            };
        }

        bool passed = value >= _minCoverage;
        return new AnalysisResult
        {
            RuleName = RuleName,
            Passed = passed,
            Details = $"Coverage = {value}% (required >= {_minCoverage}%)"
        };
    }
}

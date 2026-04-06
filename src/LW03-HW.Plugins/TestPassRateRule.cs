using LW03_HW.Core;
using LW03_HW.Core.Interfaces;

namespace LW03_HW.Plugins;

public class TestPassRateRule : IAnalysisRule
{
    private readonly double _minPassRate;
    public TestPassRateRule()
    {
        _minPassRate = 95.0;
    }

    public string RuleName => "TestPassRate";

    public AnalysisResult Check(Dictionary<string, double> metrics)
    {
        if (!metrics.TryGetValue("testpassrate", out double value))
        {
            return new AnalysisResult
            {
                RuleName = RuleName,
                Passed = false,
                Details = "Metric 'testpassrate' not found."
            };
        }

        bool passed = value >= _minPassRate;
        return new AnalysisResult
        {
            RuleName = RuleName,
            Passed = passed,
            Details = $"Test pass rate = {value}% (required >= {_minPassRate}%)"
        };
    }
}

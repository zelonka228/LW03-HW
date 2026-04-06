using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Rules;

public class DuplicationRule : IAnalysisRule
{
    private readonly double _maxDuplication;

    public DuplicationRule(double maxDuplication = 10.0)
    {
        _maxDuplication = maxDuplication;
    }


    public string RuleName => "Duplication";

    public AnalysisResult Check(Dictionary<string, double> metrics)
    {
        if (!metrics.TryGetValue("duplication", out double value))
        {
            return new AnalysisResult
            {
                RuleName = RuleName,
                Passed = false,
                Details = "Metric 'duplication' not found."
            };
        }

        bool passed = value <= _maxDuplication;
        return new AnalysisResult
        {
            RuleName = RuleName,
            Passed = passed,
            Details = $"Duplication = {value}% (required <= {_maxDuplication}%)"
        };
    }
}

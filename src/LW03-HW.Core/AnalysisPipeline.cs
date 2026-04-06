using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core;

public class AnalysisPipeline
{
    private readonly IMetricParser _parser;
    private readonly List<IAnalysisRule> _rules;
    private readonly IReporter _reporter;

    public AnalysisPipeline(IMetricParser parser, List<IAnalysisRule> rules, IReporter reporter)
    {
        _parser = parser;
        _rules = rules;
        _reporter = reporter;
    }
    public void Run(string input)
    {
 
        var metrics = _parser.Parse(input);

        var results = new List<AnalysisResult>();
        foreach (var rule in _rules)
        {
            var result = rule.Check(metrics);
            results.Add(result);
        }

        _reporter.Report(results);
    }
}

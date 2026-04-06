
using QualityGate.Core.Factories;
using QualityGate.Core.Rules;
using QualityGate.Core.Decorators;
using QualityGate.Core.Reporters;
using QualityGate.Core.Observers;

var parserFactory = new ParserFactory();
var parser = parserFactory.Create("kv");

var metrics = parser.Parse("coverage=75,duplication=5");

var rules = new List<QualityGate.Core.Interfaces.IAnalysisRule>
{
    new RuleDecorator(new CoverageRule())
};

var results = new List<(string, bool)>();

foreach (var rule in rules)
{
    results.Add((rule.Name, rule.Evaluate(metrics)));
}

var reporter = new ConsoleReporter();
reporter.Subscribe(new LoggerObserver());
reporter.Report(results);

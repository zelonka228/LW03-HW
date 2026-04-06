using LW03_HW.Core;
using LW03_HW.Core.Parsers;
using LW03_HW.Core.Rules;
using LW03_HW.Core.Reporters;


Console.WriteLine("Quality Gate Analyzer");
Console.WriteLine("Usage: dotnet run -- [inline|file] [input] [console|json]");
Console.WriteLine();

string format = args.Length > 0 ? args[0] : "inline";
string input  = args.Length > 1 ? args[1] : "coverage=85,duplication=5,complexity=8";
string reportFormat = args.Length > 2 ? args[2] : "console";

try
{
 
    var parserFactory = new ParserFactory();
    var parser = parserFactory.Create(format);

    var rules = new List<LW03_HW.Core.Interfaces.IAnalysisRule>
    {
        new RuleDecorator(new CoverageRule(80.0)),
        new RuleDecorator(new DuplicationRule(10.0)),
        new RuleDecorator(new ComplexityRule(10.0))
    };

    var reporterFactory = new ReporterFactory();
    var reporter = reporterFactory.Create(reportFormat);

    var logger = new LoggerObserver();
    if (reporter is ConsoleReporter consoleReporter)
        consoleReporter.Subscribe(logger);
    else if (reporter is JsonReporter jsonReporter)
        jsonReporter.Subscribe(logger);

    var pipeline = new AnalysisPipeline(parser, rules, reporter);
    pipeline.Run(input);
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] {ex.Message}");
    Environment.Exit(1);
}

using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Reporters;

public class LoggerObserver : IObserver
{
    public void OnFail(AnalysisResult result)
    {
        Console.WriteLine($"[ALERT] FAIL detected in rule '{result.RuleName}': {result.Details}");
    }
}

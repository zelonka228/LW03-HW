namespace LW03_HW.Core.Interfaces;


public interface IAnalysisRule
{

    string RuleName { get; }

    AnalysisResult Check(Dictionary<string, double> metrics);
}

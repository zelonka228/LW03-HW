namespace LW03_HW.Core.Interfaces;

public interface IMetricParser
{
    Dictionary<string, double> Parse(string input);
}

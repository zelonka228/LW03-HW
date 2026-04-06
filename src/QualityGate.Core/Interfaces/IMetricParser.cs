
namespace QualityGate.Core.Interfaces;

public interface IMetricParser
{
    Dictionary<string, int> Parse(string input);
}

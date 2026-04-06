
using QualityGate.Core.Interfaces;
using QualityGate.Core.Parsers;

namespace QualityGate.Core.Factories;

public class ParserFactory
{
    public IMetricParser Create(string type)
    {
        if (type == "kv") return new KeyValueParser();
        throw new Exception("Unknown parser");
    }
}

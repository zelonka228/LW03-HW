
using QualityGate.Core.Interfaces;

namespace QualityGate.Core.Parsers;

public class KeyValueParser : IMetricParser
{
    public Dictionary<string, int> Parse(string input)
    {
        var dict = new Dictionary<string, int>();
        var pairs = input.Split(',');

        foreach (var p in pairs)
        {
            var kv = p.Split('=');
            if (kv.Length != 2)
                throw new Exception("Invalid format");

            dict[kv[0].Trim()] = int.Parse(kv[1]);
        }

        return dict;
    }
}

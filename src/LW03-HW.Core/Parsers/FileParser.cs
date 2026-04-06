using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Parsers;

public class FileParser : IMetricParser
{
    private readonly KeyValueParser _innerParser = new KeyValueParser();

    public Dictionary<string, double> Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("File path cannot be empty.");

        if (!File.Exists(input))
            throw new ArgumentException($"File not found: '{input}'");

        var content = File.ReadAllText(input).Trim();
        return _innerParser.Parse(content);
    }
}

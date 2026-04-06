using LW03_HW.Core.Interfaces;
using LW03_HW.Core.Parsers;

namespace LW03_HW.Core.Parsers;

public class ParserFactory
{

    public IMetricParser Create(string format)
    {
        if (string.IsNullOrWhiteSpace(format))
            throw new ArgumentException("Format cannot be empty.");

        switch (format.ToLower())
        {
            case "file":
                return new FileParser();
            case "inline":
                return new KeyValueParser();
            default:
                throw new ArgumentException($"Unknown parser format: '{format}'. Use 'file' or 'inline'.");
        }
    }
}

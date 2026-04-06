using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Parsers;

public class KeyValueParser : IMetricParser
{
    public Dictionary<string, double> Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be empty.");

        var result = new Dictionary<string, double>();

        var pairs = input.Split(',');

        foreach (var pair in pairs)
        {
            var parts = pair.Trim().Split('=');

            if (parts.Length != 2)
                throw new ArgumentException($"Invalid format in: '{pair}'. Expected key=value.");

            var key = parts[0].Trim();
            var valueStr = parts[1].Trim();

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty.");

            if (!double.TryParse(valueStr, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double value))
                throw new ArgumentException($"Value '{valueStr}' for key '{key}' is not a valid number.");

            result[key] = value;
        }

        return result;
    }
}

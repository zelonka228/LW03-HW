using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Reporters;
public class ReporterFactory
{
    public IReporter Create(string format)
    {
        if (string.IsNullOrWhiteSpace(format))
            throw new ArgumentException("Reporter format cannot be empty.");

        switch (format.ToLower())
        {
            case "console":
                return new ConsoleReporter();
            case "json":
                return new JsonReporter();
            default:
                throw new ArgumentException($"Unknown reporter format: '{format}'. Use 'console' or 'json'.");
        }
    }
}

using System.Reflection;
using LW03_HW.Core.Interfaces;

namespace LW03_HW.Core.Plugins;

public class PluginLoader
{
    public List<IAnalysisRule> LoadRules(string assemblyPath)
    {
        if (string.IsNullOrWhiteSpace(assemblyPath))
            throw new ArgumentException("Assembly path cannot be empty.");

        if (!File.Exists(assemblyPath))
            throw new ArgumentException($"Assembly file not found: '{assemblyPath}'");

        var rules = new List<IAnalysisRule>();

        var assembly = Assembly.LoadFrom(assemblyPath);

        var ruleTypes = assembly.GetTypes()
            .Where(t => typeof(IAnalysisRule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in ruleTypes)
        {

            var instance = Activator.CreateInstance(type) as IAnalysisRule;
            if (instance != null)
            {
                rules.Add(instance);
            }
        }

        return rules;
    }
}

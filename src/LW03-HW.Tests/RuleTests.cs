using LW03_HW.Core.Rules;
using Xunit;

namespace LW03_HW.Tests;

public class CoverageRuleTests
{
    [Fact]
    public void Check_CoverageAboveThreshold_ReturnsPassed()
    {
        var rule = new CoverageRule(80.0);
        var metrics = new Dictionary<string, double> { { "coverage", 90.0 } };

        var result = rule.Check(metrics);

        Assert.True(result.Passed);
    }

    [Fact]
    public void Check_CoverageEqualThreshold_ReturnsPassed()
    {
        var rule = new CoverageRule(80.0);
        var metrics = new Dictionary<string, double> { { "coverage", 80.0 } };

        var result = rule.Check(metrics);

        Assert.True(result.Passed);
    }

    [Fact]
    public void Check_CoverageBelowThreshold_ReturnsFailed()
    {
        var rule = new CoverageRule(80.0);
        var metrics = new Dictionary<string, double> { { "coverage", 72.0 } };

        var result = rule.Check(metrics);

        Assert.False(result.Passed);
    }

    [Fact]
    public void Check_MissingMetric_ReturnsFailed()
    {
        var rule = new CoverageRule(80.0);
        var metrics = new Dictionary<string, double>();

        var result = rule.Check(metrics);

        Assert.False(result.Passed);
        Assert.Contains("not found", result.Details);
    }

    [Fact]
    public void RuleName_IsCoverage()
    {
        var rule = new CoverageRule();
        Assert.Equal("Coverage", rule.RuleName);
    }
}

public class DuplicationRuleTests
{
    [Fact]
    public void Check_DuplicationBelowThreshold_ReturnsPassed()
    {
        var rule = new DuplicationRule(10.0);
        var metrics = new Dictionary<string, double> { { "duplication", 5.0 } };

        var result = rule.Check(metrics);

        Assert.True(result.Passed);
    }

    [Fact]
    public void Check_DuplicationAboveThreshold_ReturnsFailed()
    {
        var rule = new DuplicationRule(10.0);
        var metrics = new Dictionary<string, double> { { "duplication", 18.0 } };

        var result = rule.Check(metrics);

        Assert.False(result.Passed);
    }

    [Fact]
    public void Check_DuplicationEqualThreshold_ReturnsPassed()
    {
        var rule = new DuplicationRule(10.0);
        var metrics = new Dictionary<string, double> { { "duplication", 10.0 } };

        var result = rule.Check(metrics);

        Assert.True(result.Passed);
    }

    [Fact]
    public void Check_MissingMetric_ReturnsFailed()
    {
        var rule = new DuplicationRule();
        var result = rule.Check(new Dictionary<string, double>());

        Assert.False(result.Passed);
    }
}

public class ComplexityRuleTests
{
    [Fact]
    public void Check_ComplexityBelowThreshold_ReturnsPassed()
    {
        var rule = new ComplexityRule(10.0);
        var metrics = new Dictionary<string, double> { { "complexity", 7.0 } };

        var result = rule.Check(metrics);

        Assert.True(result.Passed);
    }

    [Fact]
    public void Check_ComplexityAboveThreshold_ReturnsFailed()
    {
        var rule = new ComplexityRule(10.0);
        var metrics = new Dictionary<string, double> { { "complexity", 15.0 } };

        var result = rule.Check(metrics);

        Assert.False(result.Passed);
    }

    [Fact]
    public void Check_MissingMetric_ReturnsFailed()
    {
        var rule = new ComplexityRule();
        var result = rule.Check(new Dictionary<string, double>());

        Assert.False(result.Passed);
    }
}

public class RuleDecoratorTests
{
    [Fact]
    public void Check_WrappedRule_ReturnsCorrectResult()
    {
        var innerRule = new CoverageRule(80.0);
        var decorator = new RuleDecorator(innerRule);
        var metrics = new Dictionary<string, double> { { "coverage", 90.0 } };

        var result = decorator.Check(metrics);

        Assert.True(result.Passed);
    }

    [Fact]
    public void RuleName_MatchesInnerRule()
    {
        var innerRule = new CoverageRule();
        var decorator = new RuleDecorator(innerRule);

        Assert.Equal("Coverage", decorator.RuleName);
    }

    [Fact]
    public void Check_DecoratorDoesNotChangeLogic_FailStillFails()
    {
        var innerRule = new CoverageRule(80.0);
        var decorator = new RuleDecorator(innerRule);
        var metrics = new Dictionary<string, double> { { "coverage", 50.0 } };

        var result = decorator.Check(metrics);

        Assert.False(result.Passed);
    }
}

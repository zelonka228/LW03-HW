
using Xunit;
using QualityGate.Core.Parsers;
using QualityGate.Core.Rules;

public class AllTests
{
    [Fact]
    public void Parser_Works()
    {
        var p = new KeyValueParser();
        var r = p.Parse("coverage=80");
        Assert.Equal(80, r["coverage"]);
    }

    [Fact]
    public void Rule_Pass()
    {
        var rule = new CoverageRule();
        var metrics = new Dictionary<string,int>{{"coverage",90}};
        Assert.True(rule.Evaluate(metrics));
    }

    [Fact]
    public void Rule_Fail()
    {
        var rule = new CoverageRule();
        var metrics = new Dictionary<string,int>{{"coverage",10}};
        Assert.False(rule.Evaluate(metrics));
    }
}

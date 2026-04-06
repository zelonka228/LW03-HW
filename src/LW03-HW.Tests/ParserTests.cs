using LW03_HW.Core.Parsers;
using Xunit;

namespace LW03_HW.Tests;

public class ParserTests
{
    private readonly KeyValueParser _parser = new KeyValueParser();

    [Fact]
    public void Parse_ValidInput_ReturnsCorrectDictionary()
    {
        var result = _parser.Parse("coverage=72,duplication=18");

        Assert.Equal(72.0, result["coverage"]);
        Assert.Equal(18.0, result["duplication"]);
    }

    [Fact]
    public void Parse_SingleEntry_Works()
    {
        var result = _parser.Parse("coverage=80");

        Assert.Equal(80.0, result["coverage"]);
    }

    [Fact]
    public void Parse_EmptyInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _parser.Parse(""));
    }

    [Fact]
    public void Parse_NullInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _parser.Parse(null!));
    }

    [Fact]
    public void Parse_MissingEquals_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _parser.Parse("coverage72"));
    }

    [Fact]
    public void Parse_NonNumericValue_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _parser.Parse("coverage=abc"));
    }

    [Fact]
    public void Parse_SpacesAroundKeyValue_Works()
    {
        var result = _parser.Parse(" coverage = 90 ");

        Assert.Equal(90.0, result["coverage"]);
    }

    [Fact]
    public void Parse_ThreeMetrics_AllParsed()
    {
        var result = _parser.Parse("coverage=85,duplication=5,complexity=8");

        Assert.Equal(3, result.Count);
        Assert.Equal(85.0, result["coverage"]);
        Assert.Equal(5.0, result["duplication"]);
        Assert.Equal(8.0, result["complexity"]);
    }
}

public class ParserFactoryTests
{
    private readonly ParserFactory _factory = new ParserFactory();

    [Fact]
    public void Create_InlineFormat_ReturnsKeyValueParser()
    {
        var parser = _factory.Create("inline");

        Assert.IsType<KeyValueParser>(parser);
    }

    [Fact]
    public void Create_FileFormat_ReturnsFileParser()
    {
        var parser = _factory.Create("file");

        Assert.IsType<FileParser>(parser);
    }

    [Fact]
    public void Create_UnknownFormat_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _factory.Create("xml"));
    }

    [Fact]
    public void Create_EmptyFormat_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _factory.Create(""));
    }

    [Fact]
    public void Create_UppercaseFormat_Works()
    {
        var parser = _factory.Create("INLINE");

        Assert.IsType<KeyValueParser>(parser);
    }
}

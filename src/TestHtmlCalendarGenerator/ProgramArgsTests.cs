using HtmlCalendarGenerator;
using static HtmlCalendarGenerator.Constants;

namespace TestHtmlCalendarGenerator;

[TestClass]
public class ProgramArgsTests
{
    [DataTestMethod]
    [DynamicData(nameof(DataOne))]
    public void Given_program_arguments_Then_should_parse_them(string[] arguments, Option[] expected)
    {
        // Arrange
        var underTest = new ProgramArgs();

        // Act
        var actual = underTest.Parse(arguments).ToArray();

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [DataTestMethod]
    [DynamicData(nameof(DataTwo))]
    public void Given_program_arguments_Then_should_check_for_required_arguments(string[] arguments, bool expected)
    {
        // Arrange
        var underTest = new ProgramArgs();

        // Act
        var actual = underTest.RequiredOptionsExist(underTest.Parse(arguments));

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #region Detail

    private static IEnumerable<object[]> DataOne
    {
        get
        {
            yield return new object[] { default, Array.Empty<Option>() };
            yield return new object[] { Array.Empty<string>(), Array.Empty<Option>() };
            yield return new object[]
            {
                new[]
                {
                    "blah",
                    "blah else",
                    "blah something else"
                },
                Array.Empty<Option>()
            };
            yield return new object[]
            {
                new[] { HelpOptionName },
                new Option[] { new(HelpOptionName, default) }
            };
            yield return new object[]
            {
                new[]
                {
                    OutputOptionName, 
                    "file",
                    YearOptionName, 
                    "year"
                },
                new Option[]
                {
                    new(OutputOptionName, "file"),
                    new(YearOptionName, "year")
                }
            };
        }
    }

    private static IEnumerable<object[]> DataTwo
    {
        get
        {
            yield return new object[] { new[] { HelpOptionName }, false };
            yield return new object[] { new[] { OutputOptionName }, false };
            yield return new object[] { new[] { YearOptionName }, false };
            yield return new object[]
            {
                new[]
                {
                    YearOptionName, 
                    "year", 
                    OutputOptionName,
                    "file"
                }, true
            };
            yield return new object[]
            {
                new[]
                {
                    YearOptionName,
                    "year",
                    OutputOptionName,
                    "file", 
                    HelpOptionName, 
                    "blah"
                }, true
            };
        }
    }

    #endregion
}
using CalendarGenerator;
using static CalendarGenerator.Constants;

namespace TestCalendarGenerator;

[TestClass]
public class OptionsFactoryTests
{
    [DataTestMethod]
    [DynamicData(nameof(DataOne))]
    public void Given_program_args_Then_should_create_options(string[] args, Option[] expected)
    {
        // Arrange
        var underTest = new OptionsFactory();

        // Act
        var actual = underTest.CreateOptions(args).ToArray();

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [DataTestMethod]
    [DynamicData(nameof(DataTwo))]
    public void Given_program_args_Then_should_check_for_required_options(string[] args, bool expected)
    {
        // Arrange
        var underTest = new OptionsFactory();

        // Act
        var actual = underTest.RequiredOptionsExist(underTest.CreateOptions(args));

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
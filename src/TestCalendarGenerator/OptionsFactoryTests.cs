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
                new[] { HelpOption },
                new Option[] { new(HelpOption, default) }
            };
            yield return new object[]
            {
                new[]
                {
                    OutputOption,
                    "file",
                    YearOption,
                    "year"
                },
                new Option[]
                {
                    new(OutputOption, "file"),
                    new(YearOption, "year")
                }
            };
        }
    }

    private static IEnumerable<object[]> DataTwo
    {
        get
        {
            yield return new object[] { new[] { HelpOption }, false };
            yield return new object[] { new[] { OutputOption }, false };
            yield return new object[] { new[] { YearOption }, false };
            yield return new object[]
            {
                new[]
                {
                    YearOption,
                    "year",
                    OutputOption,
                    "file"
                }, true
            };
            yield return new object[]
            {
                new[]
                {
                    YearOption,
                    "year",
                    OutputOption,
                    "file",
                    HelpOption,
                    "blah"
                }, true
            };
        }
    }

    #endregion
}
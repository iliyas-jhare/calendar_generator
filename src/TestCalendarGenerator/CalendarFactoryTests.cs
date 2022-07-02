using CalendarGenerator;

namespace TestCalendarGenerator;

[TestClass]
public class CalendarFactoryTests
{
    [TestMethod]
    public void Given_program_option_Then_should_create_calendar_years()
    {
        // Arrange
        var underTest = new CalendarFactory();

        // Act
        var actual = underTest.CreateCalendarYears(new(Constants.YearOption, "2022"));

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.Any());
    }

    #region Detail

    #endregion
}
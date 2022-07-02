using CalendarGenerator;

namespace TestCalendarGenerator;

[TestClass]
public class CalendarYearFactoryTests
{
    [TestMethod]
    public void Given_program_option_Then_should_create_calendar_years()
    {
        // Arrange
        var underTest = new CalendarYearFactory(new CalendarDayFactory());

        // Act
        var actual = underTest.CreateCalendarYears(new(Constants.YearOption, "2022"));

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.Any());
    }

    #region Detail

    #endregion
}
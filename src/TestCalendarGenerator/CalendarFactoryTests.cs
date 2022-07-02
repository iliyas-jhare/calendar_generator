using CalendarGenerator;

namespace TestCalendarGenerator;

[TestClass]
public class CalendarFactoryTests
{
    [TestMethod]
    public void Given_When_Then()
    {
        // Arrange
        var underTest = new CalendarFactory();

        // Act
        var actual = underTest.CreateCalendarYears(new(Constants.YearOptionName, "2022"));

        // Assert
        var html = new HtmlWriter().CreateCalendar(actual);
    }

    #region Detail

    #endregion
}
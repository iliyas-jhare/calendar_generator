using CalendarGenerator;
using System.Diagnostics;

namespace TestCalendarGenerator;

[TestClass]
public class HtmlWriterTests
{
    [TestMethod]
    public void Given_calendar_month_Then_should_create_html_correctly()
    {
        // Arrange 
        var underTest = new HtmlWriter();

        // Act
        var actual = underTest.CreateCalendar(MonthData);
        Debug.WriteLine(actual);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(actual));
    }

    [TestMethod]
    public void Given_calendar_quarter_year_Then_should_create_html_correctly()
    {
        // Arrange 
        var underTest = new HtmlWriter();

        // Act
        var actual = underTest.CreateCalendar(YearData);
        Debug.WriteLine(actual);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(actual));
    }

    #region Detail

    private static CalendarMonth MonthData =>
        new(1, new CalendarWeek[]
        {
            new(1, new CalendarDay[] {new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4), new(1, 5), new(2, 6)}),
            new(2, new CalendarDay[] {new(3, 0), new(4, 1), new(5, 2), new(6, 3), new(7, 4), new(8, 5), new(9, 6)}),
            new(3,
                new CalendarDay[] {new(10, 0), new(11, 1), new(12, 2), new(13, 3), new(14, 4), new(15, 5), new(16, 6)}),
            new(4,
                new CalendarDay[] {new(17, 0), new(18, 1), new(19, 2), new(20, 3), new(21, 4), new(22, 5), new(23, 6)}),
            new(5,
                new CalendarDay[] {new(24, 0), new(25, 1), new(26, 2), new(27, 3), new(28, 4), new(29, 5), new(30, 6)}),
            new(6, new CalendarDay[] {new(31, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4), new(0, 5), new(0, 6)})
        });

    private static CalendarYear YearData =>
        new(1111, new[] { MonthData });

    #endregion
}
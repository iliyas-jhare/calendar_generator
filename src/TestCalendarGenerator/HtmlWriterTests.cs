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
        var actual = underTest.CreateCalendar(October);
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
        var actual = underTest.CreateCalendar(QuarterYear);
        Debug.WriteLine(actual);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(actual));
    }

    #region Detail

    private static CalendarMonth October =>
        new(10, new CalendarWeek[]
        {
            new(39, new CalendarDay[] {new(0), new(0), new(0), new(0), new(0), new(1), new(2)}),
            new(40, new CalendarDay[] {new(3), new(4), new(5), new(6), new(7), new(8), new(9)}),
            new(41, new CalendarDay[] {new(10), new(11), new(12), new(13), new(14), new(15), new(16)}),
            new(42, new CalendarDay[] {new(17), new(18), new(19), new(20), new(21), new(22), new(23)}),
            new(43, new CalendarDay[] {new(24), new(25), new(26), new(27), new(28), new(29), new(30)}),
            new(44, new CalendarDay[] {new(31), new(0), new(0), new(0), new(0), new(0), new(0)})
        });

    private static CalendarMonth November =>
        new(11, new CalendarWeek[]
        {
            new(44, new CalendarDay[] {new(0), new(1), new(2), new(3), new(4), new(5), new(6)}),
            new(45, new CalendarDay[] {new(7), new(8), new(9), new(10), new(11), new(12), new(13)}),
            new(46, new CalendarDay[] {new(14), new(15), new(16), new(17), new(18), new(19), new(20)}),
            new(47, new CalendarDay[] {new(21), new(22), new(23), new(24), new(25), new(26), new(27)}),
            new(48, new CalendarDay[] {new(28), new(29), new(30), new(0), new(0), new(0), new(0)}),
        });

    private static CalendarMonth December =>
        new(12, new CalendarWeek[]
        {
            new(48, new CalendarDay[] {new(0), new(0), new(0), new(1), new(2), new(3), new(4)}),
            new(49, new CalendarDay[] {new(5), new(6), new(7), new(8), new(9), new(10), new(11)}),
            new(50, new CalendarDay[] {new(12), new(13), new(14), new(15), new(16), new(17), new(18)}),
            new(51, new CalendarDay[] {new(19), new(20), new(21), new(22), new(23), new(24), new(25)}),
            new(52, new CalendarDay[] {new(26), new(27), new(28), new(29), new(30), new(31), new(0)}),
        });

    private static CalendarYear QuarterYear =>
        new(2022, new[] { October, November, December });

    #endregion
}
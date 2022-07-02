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
        Assert.AreEqual(3, actual.Count);  // 3 years
        Assert.AreEqual(36, actual.Sum(y => y.Months.Count));  // 36 months for 3 years
        Assert.AreEqual(1095,
            actual.Sum(y =>
                y.Months.Sum(m =>
                    m.Weeks.Sum(w => w.Days.Count(d => d.Number != 0))))); // 1065 days for 3 years 
    }
}
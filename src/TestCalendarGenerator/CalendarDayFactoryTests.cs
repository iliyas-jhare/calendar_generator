using CalendarGenerator;

namespace TestCalendarGenerator;

[TestClass]
public class CalendarDayFactoryTests
{
    [DataTestMethod]
    [DynamicData(nameof(DataOne))]
    public void Given_year_and_month_Then_should_create_calendar_days(int year, int month, int weeks, int days, int firstWeekDays, int lastWeekDays)
    {
        // Arrange
        var underTest = new CalendarDayFactory();

        // Act
        var actual = underTest.CreateCalendarDays(year, month);

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.Any());
        Assert.AreEqual(weeks, actual.Count);
        Assert.AreEqual(days, actual.Sum(w => w.Count));  // including empty days
        Assert.AreEqual(firstWeekDays, actual.First().Count(d => d.Number != 0));
        Assert.AreEqual(lastWeekDays, actual.Last().Count(d => d.Number != 0));
    }

    private static IEnumerable<object[]> DataOne
    {
        get
        {
            yield return new object[] { 2020, 2, 5, 35, 2, 6 };
            yield return new object[] { 2021, 1, 5, 35, 3, 7 };
            yield return new object[] { 2021, 2, 4, 28, 7, 7 };
            yield return new object[] { 2022, 10, 6, 42, 2, 1 };
            yield return new object[] { 2023, 11, 5, 35, 5, 4 };
        }
    }
}
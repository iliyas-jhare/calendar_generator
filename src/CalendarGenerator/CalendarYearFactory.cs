namespace CalendarGenerator;

public class CalendarYearFactory
{
    private readonly CalendarDayFactory calendarDayFactory;
    private int weeksCounter;

    private IReadOnlyList<int> MonthNumbers { get; }

    public CalendarYearFactory(CalendarDayFactory calendarDayFactory)
    {
        this.calendarDayFactory = calendarDayFactory;
        weeksCounter = 1;
        MonthNumbers = Enumerable.Range(1, 12).ToArray();
    }

    public IReadOnlyList<CalendarYear> CreateCalendarYears(Option option)
    {
        if (option is null ||
            !option.Is(Constants.YearOption) ||
            string.IsNullOrEmpty(option.Value))
        {
            throw new ArgumentException("Invalid option found.", nameof(option));
        }

        return GetYears(option)
            .Select(year =>
            {
                // reset weeks counter
                weeksCounter = 1;
                return new CalendarYear(year, CreateCalendarMonths(year));
            })
            .ToArray();
    }

    #region Detail

    private IReadOnlyList<CalendarMonth> CreateCalendarMonths(int year) =>
        MonthNumbers.Select(month => new CalendarMonth(month, CreateCalendarWeeks(year, month))).ToArray();

    private IReadOnlyList<CalendarWeek> CreateCalendarWeeks(int year, int month) =>
        CreateCalendarWeeks(calendarDayFactory.CreateCalendarDays(year, month));

    private IReadOnlyList<CalendarWeek> CreateCalendarWeeks(IReadOnlyList<IReadOnlyList<CalendarDay>> daysOfMonth)
    {
        var weeks = new List<CalendarWeek>();
        for (var i = 0; i < daysOfMonth.Count; i++)
        {
            var days = daysOfMonth[i];
            // first week of month and empty
            if (i == 0 && ContainsEmpty(days))
            {
                weeks.Add(new(weeksCounter, days));
            }
            // last week of month and empty
            else if (i == daysOfMonth.Count - 1 && ContainsEmpty(days))
            {
                weeks.Add(new(++weeksCounter, days));
            }
            else
            {
                weeks.Add(new(++weeksCounter, days));
            }
        }

        return weeks;
    }

    private static bool ContainsEmpty(IEnumerable<CalendarDay> days) =>
        days.Any(d => d.Number == 0);

    private static IReadOnlyList<int> GetYears(Option option)
    {
        var year = Convert.ToInt32(option.Value);
        return new[] { year - 1, year, year + 1 };
    }

    #endregion
}
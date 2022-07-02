using System.Globalization;

namespace CalendarGenerator;

public class CalendarFactory
{
    private const int Dimension = 7;

    private int weekCounter;

    private IReadOnlyList<int> MonthNumbers { get; }

    public CalendarFactory()
    {
        weekCounter = 1;
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
                weekCounter = 1;
                return new CalendarYear(year, CreateCalendarMonths(year));
            })
            .ToArray();
    }

    #region Detail

    private IReadOnlyList<CalendarMonth> CreateCalendarMonths(int year) =>
        MonthNumbers.Select(month => new CalendarMonth(month, CreateCalendarWeeks(year, month))).ToArray();

    private IReadOnlyList<CalendarWeek> CreateCalendarWeeks(int year, int month)
    {
        var table = CreateTable(year, month);
        var daysOfMonth = CreateCalendarDays(table);
        return CreateCalendarWeeks(daysOfMonth);
    }

    private IReadOnlyList<CalendarWeek> CreateCalendarWeeks(IReadOnlyList<IReadOnlyList<CalendarDay>> daysOfMonth)
    {
        var weeks = new List<CalendarWeek>();
        for (var i = 0; i < daysOfMonth.Count; i++)
        {
            var days = daysOfMonth[i];
            if (i == 0 && ContainsEmpty(days))
            {
                weeks.Add(new(weekCounter, days));
            }
            else if (i == daysOfMonth.Count - 1 && ContainsEmpty(days))
            {
                weeks.Add(new(++weekCounter, days));
            }
            else
            {
                weeks.Add(new(++weekCounter, days));
            }
        }

        return weeks;
    }

    private static bool ContainsEmpty(IEnumerable<CalendarDay> days) =>
        days.Any(d => d.Number == 0);

    private static IReadOnlyList<IReadOnlyList<CalendarDay>> CreateCalendarDays(int[,] table)
    {
        var daysOfMonth = new List<IReadOnlyList<CalendarDay>>();
        for (var i = 0; i < table.GetLength(0); i++)
        {
            var days = new List<CalendarDay>();
            for (var j = 0; j < table.GetLength(1); j++)
            {
                days.Add(new CalendarDay(table[i, j]));
            }

            if (days.All(d => d.Number <= 0)) continue;

            daysOfMonth.Add(days);
        }

        return daysOfMonth;
    }

    private static int[,] CreateTable(int year, int month)
    {
        var table = new int[Dimension, Dimension];
        var firstDate = new DateTime(year, month, 1, new GregorianCalendar());
        var firstDayOfWeek = (int)firstDate.DayOfWeek;
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var nextDay = 1;
        for (var i = 0; i < table.GetLength(0); i++)
        {
            for (var j = 0; j < table.GetLength(1) && nextDay - firstDayOfWeek + 1 <= daysInMonth; j++)
            {
                if (i == 0 && month > j)
                {
                    table[i, j] = 0;
                }
                else
                {
                    table[i, j] = nextDay - firstDayOfWeek + 1;
                    nextDay++;
                }
            }
        }

        return table;
    }

    private static IReadOnlyList<int> GetYears(Option option)
    {
        var year = Convert.ToInt32(option.Value);
        return new[] { year - 1, year, year + 1 };
    }

    #endregion
}

using System.Globalization;

namespace CalendarGenerator;

public class CalendarDayFactory
{
    private const int NumberOfWeekDays = 7;

    public IReadOnlyList<IReadOnlyList<CalendarDay>> CreateCalendarDays(int year, int month) =>
        CreateCalendarDaysPerWeekInMonth(year, month);

    #region Detail

    private IReadOnlyList<IReadOnlyList<CalendarDay>> CreateCalendarDaysPerWeekInMonth(int year, int month)
    {
        var calendarDaysPerWeekInMonth = new List<List<CalendarDay>>();
        var calendarDaysInMonth = CreateCalendarDaysInMonth(year, month);
        var calendarDaysInWeek = new List<CalendarDay>();
        foreach (var calendarDay in calendarDaysInMonth)
        {
            if (calendarDay.DayOfWeek == 0 && calendarDaysInWeek.Any())
            {
                calendarDaysPerWeekInMonth.Add(calendarDaysInWeek);
                calendarDaysInWeek = new List<CalendarDay>();
            }

            calendarDaysInWeek.Add(calendarDay);
        }

        if (calendarDaysInWeek.Any())
        {
            calendarDaysPerWeekInMonth.Add(calendarDaysInWeek);
        }

        foreach (var calendarDays in calendarDaysPerWeekInMonth.Where(calendarDays => calendarDays.Count < NumberOfWeekDays))
        {
            AddEmptyCalendarDays(calendarDays);
        }

        return calendarDaysPerWeekInMonth;
    }

    private static void AddEmptyCalendarDays(List<CalendarDay> calendarDays)
    {
        if (!calendarDays.Any()) return;

        for (var i = 0; i < NumberOfWeekDays; i++)
        {
            if (calendarDays.All(d => d.DayOfWeek != i))
            {
                calendarDays.Add(new(0, i));
            }
        }

        calendarDays.Sort(new CalendarDayComparer());
    }

    private static IReadOnlyList<CalendarDay> CreateCalendarDaysInMonth(int year, int month)
    {
        var calendarDaysInMonth = new List<CalendarDay>();
        var daysInMonth = DateTime.DaysInMonth(year, month);
        for (var day = 1; day <= daysInMonth; day++)
        {
            var date = new DateTime(year, month, day, new GregorianCalendar());
            var dayOfWeek = (int)date.DayOfWeek;
            // make Mon the first day of the week
            calendarDaysInMonth.Add(new(day, dayOfWeek == 0 ? 6 : dayOfWeek - 1));
        }

        return calendarDaysInMonth;
    }

    #endregion
}
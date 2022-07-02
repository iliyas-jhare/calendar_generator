using System.Globalization;

namespace CalendarGenerator;

public class CalendarDayFactory
{
    private const int Rows = 7, Columns = 7;

    public IReadOnlyList<IReadOnlyList<CalendarDay>> CreateCalendarDays(int year, int month)
    {
        var table = CreateTable(year, month);
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
        var table = new int[Rows, Columns];
        var firstDate = new DateTime(year, month, 1, new GregorianCalendar());
        var firstDayOfWeek = (int) firstDate.DayOfWeek;
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
}
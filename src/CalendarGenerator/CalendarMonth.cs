using System.Globalization;

namespace CalendarGenerator;

public record CalendarMonth(int Number, IReadOnlyList<CalendarWeek> Weeks)
{
    public string Name => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Number);
}
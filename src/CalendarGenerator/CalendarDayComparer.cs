namespace CalendarGenerator;

public class CalendarDayComparer : IComparer<CalendarDay>
{
    public int Compare(CalendarDay a, CalendarDay b) =>
        a is null || b is null ? 1 : a.DayOfWeek.CompareTo(b.DayOfWeek);
}